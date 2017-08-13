using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public interface IGameSystem
	{
		void Initialize();
		void Release();
	}

	public interface IGameSystemUpdatable
	{
		void SystemUpdate();
	}
}

