using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class IUserInterface<T> : ISingleton<IUserInterface<T>>, ISystem, ISystemUpdatable
	{
		public virtual void Initialize()
		{

		}

		public virtual void SystemUpdate()
		{

		}

		public virtual void Release()
		{

		}
	}
}
