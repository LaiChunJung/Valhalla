using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public interface ISystem
	{
		/// <summary>
		/// Initialize會在呼叫AddSystem<T>()時執行.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Release會在呼叫MainManager.RomoveSystem<T>()時執行.
		/// </summary>
		void Release();
	}

	public interface ISystemUpdatable
	{
		/// <summary>
		/// SystemUpdate會在呼叫MainManager.SystemUpdate()時執行.
		/// </summary>
		void SystemUpdate();
	}
}

