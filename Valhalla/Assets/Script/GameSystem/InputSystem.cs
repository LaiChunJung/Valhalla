using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	//[DisallowMultipleComponent]
	public class InputSystem : MonoBehaviour, ISystem, ISystemUpdatable
	{
		private bool InputSwitch = true;
		
		public void Initialize()
		{
			
		}

		public void SystemUpdate()
		{
			if (!InputSwitch)
				return;
		}

		public void Release ()
		{
			InputSwitch = false;
		}
	}
}
