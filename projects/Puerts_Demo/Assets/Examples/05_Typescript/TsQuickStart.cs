using UnityEngine;
using Puerts;
using System;

//typescript工程在TsProj，在该目录执行npm run build即可编译并把js文件拷贝进unity工程

namespace PuertsTest
{
    public class TsQuickStart : MonoBehaviour
    {
        JsEnv jsEnv;

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                if(jsEnv != null) jsEnv.Dispose();
                Debug.Log("RECREATE MODULE");
                jsEnv = new JsEnv(new DefaultLoader(@"C:\git\puerts_unity_demo\projects\Puerts_Demo\TsProj\output"){isExternal = true});
                // jsEnv = new JsEnv(new DefaultLoader(UnityEngine.Application.dataPath + "../TsProj/output/"), 8080);
                // jsEnv.WaitDebugger();
                jsEnv.ClearModuleCache();
                jsEnv.Eval("require('MyComponent')", Time.frameCount.ToString());
            }
        }

        private void Update()
        {
            jsEnv.Tick();
        }

        void OnDestroy()
        {
            jsEnv.Dispose();
        }
    }
}
