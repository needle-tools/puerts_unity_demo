using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DefaultNamespace;
using Needle.Puerts.Loaders;
using Puerts;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Needle.Puerts
{
	[ExecuteInEditMode]
	public class RuntimeHandler : MonoBehaviour
	{
		public static JsEnv Env
		{
			get
			{
				EnsureInstance();
				if (_env == null)
				{
					if (Application.isEditor)
						_needleLoader.AddFolder(@"Packages/com.needle.puerts-ts-sample/Runtime/output");
					// _needleLoader.AddFolder(Application.streamingAssetsPath);
					_loader.AddLoader(_needleLoader);
					foreach (var loaders in Instance.ScriptLoaders)
						_loader.AddLoader(loaders);
					_env = new JsEnv(_loader);
				}
				return _env;
			}
		}

		public static bool Exists
		{
			get
			{
				if (_instance) return true;
				_instance = FindObjectOfType<RuntimeHandler>();
				return _instance;
			}
		}

		public static RuntimeHandler Instance
		{
			get
			{
				EnsureInstance();
				return _instance;
			}
		}

		private static void EnsureInstance()
		{
			if (_instance) return;
			_instance = FindObjectOfType<RuntimeHandler>();
			if (!_instance)
				_instance = new GameObject("Runtime Handler").AddComponent<RuntimeHandler>();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Init()
		{
			var str = Application.streamingAssetsPath;
			if (!Directory.Exists(str)) Directory.CreateDirectory(str);
			_watches.Add(new TypescriptWatcher(str));
		}

		private static readonly NeedleLoader _needleLoader = new NeedleLoader();
		private static readonly CompoundLoader _loader = new CompoundLoader();

		private static JsEnv _env;
		private static RuntimeHandler _instance;
		private static readonly List<TypescriptWatcher> _watches = new List<TypescriptWatcher>();
		private readonly List<RegisteredComponent> components = new List<RegisteredComponent>();

		public bool DebugLogs;
		public bool AutoCollectScriptLoaders = false;
		public List<TypescriptDirectory> ScriptLoaders = new List<TypescriptDirectory>();

#if UNITY_EDITOR
		[ContextMenu(nameof(FindAllScriptLoadersInEditorProject))]
		internal void FindAllScriptLoadersInEditorProject()
		{
			ScriptLoaders.Clear();
			var loaders = AssetDatabase.FindAssets("t:" + nameof(TypescriptDirectory)).Select(AssetDatabase.GUIDToAssetPath);
			foreach (var path in loaders)
			{
				ScriptLoaders.Add(AssetDatabase.LoadAssetAtPath<TypescriptDirectory>(path));
			}
		}
#endif

		public static void ReloadComponent(string name)
		{
			if (Instance.components.Count <= 0) return;
			if (!Instance.deferredReload.Contains(name))
				Instance.deferredReload.Add(name);
			if (!Application.isPlaying)
			{
				Instance.ProcessDeferredReloadNow();
				Env.Tick();
			}
		}

		internal static int CurrentId;

#if UNITY_EDITOR
		public static JSObject RegisterEditor(JSEditor inst)
		{
			Instance.EnsureIsInit();
			var name = inst.GetType().Name;
			var reg = new RegisteredComponent(name, inst);
			// Instance.components.Add(reg);
			return reg.JsInstance;
		}
#endif

		public static JSObject RegisterComponent(IJSComponent inst)
		{
			var existing = Instance.components.FirstOrDefault(c => c.Component == inst);
			if (existing != null) return existing.JsInstance;
			Instance.EnsureIsInit();
			var name = inst.GetType().Name;
			var reg = new RegisteredComponent(name, inst);
			Instance.components.Add(reg);
			return reg.JsInstance;
		}

		private static readonly Dictionary<Type, IList<MemberInfo>> functionsCache = new Dictionary<Type, IList<MemberInfo>>();

		public static string CreateEventBindings(object comp, string jsInst, string csInst, ref IList<MemberInfo> functions)
		{
			var functionBindings = "";

			if (functions == null)
			{
				var type = comp.GetType();
				if (!functionsCache.TryGetValue(type, out functions))
				{
					functions = type.GetMembers(BindingFlags.Instance | BindingFlags.Public)
						.Where(m => m.GetCustomAttribute(typeof(JSFunction)) != null)
						.ToArray();
					functionsCache.Add(type, functions);
				}
			}

			if (functions != null)
			{
				foreach (var field in functions)
				{
					AddBinding(field.Name);
				}
			}

			void AddBinding(string fn) => functionBindings += $"if({jsInst}.{fn} !== undefined) {csInst}.{fn} = () => {{ {jsInst}.{fn}(); }};\n";
			return functionBindings;
		}

		private bool didInitScriptLoaders = false;
		private bool isSubscribedToPlayerLoop = false;

		private void EnsureIsInit()
		{
			if (!didInitScriptLoaders)
			{
				this.didInitScriptLoaders = true;
				foreach (var dir in ScriptLoaders)
				{
					if (!dir) continue;
					if (DebugLogs)
						Debug.Log("Init " + dir.name, dir);
					dir.Init();
				}
			}

			if (!isSubscribedToPlayerLoop)
			{
				isSubscribedToPlayerLoop = true;
				PlayerLoopHelper.AddUpdateCallback(this, this.OnEarlyUpdate, PlayerLoopEvent.EarlyUpdate);
				PlayerLoopHelper.AddUpdateCallback(this, this.OnUpdate, PlayerLoopEvent.Update);
				PlayerLoopHelper.AddUpdateCallback(this, this.OnLateUpdate, PlayerLoopEvent.PostLateUpdate);
			}
		}

#if UNITY_EDITOR
		[InitializeOnLoadMethod]
		private static void OnEditorRecompiled()
		{
			if (Exists) Instance.didInitScriptLoaders = false;
			EditorApplication.playModeStateChanged += OnPlayModeChange;
			EditorApplication.update += () => framesSincePlaymodeChanged += 1;
		}

		internal static int framesSincePlaymodeChanged;

		private static void OnPlayModeChange(PlayModeStateChange obj)
		{
			framesSincePlaymodeChanged = 0;
		}

		private void OnValidate()
		{
			if (AutoCollectScriptLoaders) FindAllScriptLoadersInEditorProject();
			didInitScriptLoaders = false;
			EnsureIsInit();
		}
#endif

		private void Update()
		{
			if (components.Count <= 0) return;
			ProcessDeferredReloadNow();
			Env.Tick();
		}

		private readonly List<string> deferredReload = new List<string>();

		private void ProcessDeferredReloadNow()
		{
			if (deferredReload.Count <= 0) return;
			if (components.Count <= 0)
			{
				deferredReload.Clear();
				return;
			}
			Env.ClearModuleCache();
			foreach (var def in deferredReload)
			{
				if (DebugLogs)
					Debug.Log("RELOAD component: " + def);
				foreach (var comp in components)
				{
					if (comp.Name == def)
					{
						comp.Recreate();
					}
				}
			}
			deferredReload.Clear();
		}

		private void OnEarlyUpdate() => InvokeEvents(PlayerLoopEvent.EarlyUpdate, components);
		private void OnUpdate() => InvokeEvents(PlayerLoopEvent.Update, components);
		private void OnLateUpdate() => InvokeEvents(PlayerLoopEvent.PostLateUpdate, components);

		private void InvokeEvents(PlayerLoopEvent evt, List<RegisteredComponent> list)
		{
			for (var index = 0; index < list.Count; index++)
			{
				var reg = list[index];
				if (!reg.Exists)
				{
					list.RemoveAt(index--);
					continue;
				}
				if (reg.Component is IJSComponent jsComponent)
				{
					if (!jsComponent.isActiveAndEnabled) continue;
					switch (evt)
					{
						case PlayerLoopEvent.EarlyUpdate:
							jsComponent.earlyUpdate?.Invoke();
							break;
						case PlayerLoopEvent.Update:
							jsComponent.update?.Invoke();
							break;
						case PlayerLoopEvent.PreLateUpdate:
						case PlayerLoopEvent.PostLateUpdate:
							jsComponent.lateUpdate?.Invoke();
							break;
					}
				}
			}
		}
	}

	public class RegisteredComponent
	{
		public bool Exists
		{
			get
			{
				if (Component is Object obj) return obj;
				return Component != null;
			}
		}

		public bool IsValid => Exists && JsInstance != null;
		public readonly string Name;
		public readonly object Component;
		public JSObject JsInstance { get; private set; }

		private IList<MemberInfo> functions;

		public RegisteredComponent(string name, object component)
		{
			Name = name;
			Component = component;
			Recreate();
		}

		private const string jsInst = "inst";
		private const string csInst = "bind";

		public void Recreate()
		{
#if UNITY_EDITOR
			if (BuildPipeline.isBuildingPlayer) return;
#endif

			var debugLogs = RuntimeHandler.Instance.DebugLogs;

			if (debugLogs)
				Debug.Log("Recreate " + this.Name);
			var env = RuntimeHandler.Env;
			var isRecompile = JsInstance != null;
			if (isRecompile)
			{
				if (Component != null && Component is IJSComponent jsComponent)
				{
					if (jsComponent.isActiveAndEnabled)
						jsComponent.onDisable?.Invoke();
					jsComponent.onDestroy?.Invoke();
				}
			}

			var inst = Component;
			var name = Name;
			var varName = $"{name}_{RuntimeHandler.CurrentId++}";

			var eventBindings = RuntimeHandler.CreateEventBindings(this.Component, jsInst, csInst, ref functions);

			var chunk = $@"
const {varName} = require('{name}'); 
function create({csInst}){{
	const {jsInst} = new {varName}.{name}();
	{jsInst}.unity = {csInst};
	{eventBindings}
	return {jsInst};
}}; 
create
";


			if (debugLogs)
				Debug.Log("Create instance for " + name + ":\n" + chunk, inst as Object);
			var create = env.Eval<Func<object, JSObject>>(chunk, varName);
			this.JsInstance = create(inst);

			var raiseEvents = isRecompile;
#if UNITY_EDITOR
			var comp = Component as MonoBehaviour;
			var b = !Application.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode && (comp?.runInEditMode ?? false);
			var td = RuntimeHandler.framesSincePlaymodeChanged;
			b &= td > 10;
			raiseEvents |= b;
#endif
			if (raiseEvents && this.JsInstance != null)
			{
				if (Component is IJSComponent jsComponent)
				{
					jsComponent.awake?.Invoke();
					if (jsComponent.isActiveAndEnabled)
					{
						jsComponent.onEnable?.Invoke();
						jsComponent.start?.Invoke();
					}
				}
			}
		}
	}
}