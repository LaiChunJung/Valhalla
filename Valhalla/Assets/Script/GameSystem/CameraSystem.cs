using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class CameraSystem : IGameSystem, ISystemUpdatable
	{
		private Camera main;

		public override void Initialize()
		{
			base.Initialize();
		}
		
		public void SystemUpdate()
		{

		}

		public override void Release()
		{
			base.Release();
		}

		public void SetMain(Camera cam)
		{
			main = cam;
		}

		public Camera GetMain()
		{
			return main;
		}
	}
}