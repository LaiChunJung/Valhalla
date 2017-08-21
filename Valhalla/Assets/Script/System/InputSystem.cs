using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class InputSystem : IGameSystem, ISystemUpdatable
	{
		private bool InputSwitch = true;
		
		public override void Initialize()
		{
			
		}

		public void SystemUpdate()
		{
			if (!InputSwitch)
				return;
		}

		public override void Release ()
		{
			InputSwitch = false;
		}
	}
}
