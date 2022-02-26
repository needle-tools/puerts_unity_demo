using System.IO;
using System.Net.Mime;
/*
* Tencent is pleased to support the open source community by making Puerts available.
* Copyright (C) 2020 THL A29 Limited, a Tencent company.  All rights reserved.
* Puerts is licensed under the BSD 3-Clause License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
* This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
*/

namespace Puerts
{
    public interface ILoader
    {
        bool FileExists(string filepath);
        string ReadFile(string filepath, out string debugPath);
    }

    public class DefaultLoader : ILoader
    {
        private string root = "";
        public bool isExternal = false;

        public DefaultLoader()
        {
        }

        public DefaultLoader(string root)
        {
            this.root = Path.GetFullPath(root);
        }

        private string PathToUse(string filepath)
        {
            return 
            // .cjs asset is only supported in unity2018+
#if UNITY_2018_1_OR_NEWER
            filepath.EndsWith(".cjs") || filepath.EndsWith(".mjs")  ? 
                filepath.Substring(0, filepath.Length - 4) : 
#endif
                filepath;
        }

        public bool FileExists(string filepath)
        {
// #if PUERTS_GENERAL
			if(isExternal && File.Exists(Path.Combine(root, filepath))) return true;
// #else 
            string pathToUse = this.PathToUse(filepath);
            bool exist = UnityEngine.Resources.Load(pathToUse) != null;
#if !PUERTS_GENERAL && UNITY_EDITOR && !UNITY_2018_1_OR_NEWER
            if (!exist) 
            {
                UnityEngine.Debug.LogWarning("【Puerts】unity 2018- is using, if you found some js is not exist, rename *.cjs,*.mjs in the resources dir with *.cjs.txt,*.mjs.txt");
            }
#endif
            return exist;
// #endif
        }

        public string ReadFile(string filepath, out string debugPath)
        {
// #if PUERTS_GENERAL
	        if (isExternal && File.Exists(Path.Combine(root, filepath)))
	        {
	            debugPath = Path.Combine(root, filepath);
	            return File.ReadAllText(debugPath);
	        }
// #else 
            string pathToUse = this.PathToUse(filepath);
            UnityEngine.TextAsset file = (UnityEngine.TextAsset)UnityEngine.Resources.Load(pathToUse);
            
            debugPath = System.IO.Path.Combine(root, filepath);
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            debugPath = debugPath.Replace("/", "\\");
#endif
            return file == null ? null : file.text;
// #endif
        }
    }
}
