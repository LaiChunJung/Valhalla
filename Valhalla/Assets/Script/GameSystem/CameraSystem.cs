using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class CameraSystem : ISystem, ISystemUpdatable
	{
		private Camera main;

		public void Initialize()
		{
			
		}
		
		public void SystemUpdate()
		{

		}

		public void Release()
		{

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