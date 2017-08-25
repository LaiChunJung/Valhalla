using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public abstract class IGameSystem : ISystem
	{
		private bool enabled = false;

		public virtual void Initialize() { }

		public virtual void Release() { }
	}
}
