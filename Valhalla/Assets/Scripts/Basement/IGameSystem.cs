using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public interface ISystem
	{
		void Initialize();
		void Release();
	}

	public interface ISystemUpdatable
	{
		void SystemUpdate();
	}
}

