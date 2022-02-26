using System;

namespace Needle.Puerts
{
	public class JSComponentDynamic : JSComponent
	{
		public string TypeName;

		private bool didStart = false;

		protected override void Awake()
		{
			if (!didStart) return;
			base.Awake();
		}

		protected override void OnEnable()
		{
			if (!didStart) return;
			base.OnEnable();
		}

		protected override void Start()
		{
			if (!didStart)
			{
				didStart = true;
				Awake();
				OnEnable();
			}
			base.Start();
		}
	}
}