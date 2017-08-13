using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class InputSystem : MonoBehaviour, IGameSystem, IGameSystemUpdatable
	{
		[SerializeField]
		private LayerMask ClickObjectLayerMask;

		private bool InputSwitch = true;

		private delegate void InputEvent<T>(params T[] args);

		private Dictionary<string, Delegate> EventDictionary;

		public void Initialize()
		{
			EventDictionary = new Dictionary<string, Delegate>();
		}

		public void SystemUpdate()
		{
			if (!InputSwitch)
				return;

			if (EventDictionary.Count == 0)
				return;
		}

		public void Release ()
		{

		}

		private void Invoke<T> (params T[] args)
		{
			foreach (KeyValuePair<string, Delegate> val in EventDictionary)
			{
				((InputEvent<T>)val.Value)();
			}
		}

		public void AddInputEvent()
		{

		}
	}
}
