using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	[DisallowMultipleComponent]
	public class InputSystem : IGameSystem, ISystemUpdatable
	{
		private ICharacter MainCharacter;

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
