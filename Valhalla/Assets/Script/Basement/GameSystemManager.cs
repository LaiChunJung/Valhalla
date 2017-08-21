using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 系統管理器.
	/// </summary>
	public class GameSystemManager 
	{
		private GameObject Container;
		private Dictionary<Type, IGameSystem> SystemDictionary;
		private List<ISystemUpdatable> SystemUpdateList;

		private GameSystemManager() { }

		/// <summary>
		/// 建構子，初始化管理器.
		/// </summary>
		public GameSystemManager(GameObject container)
		{
			Container = container;

			SystemDictionary = new Dictionary<Type, IGameSystem>();

			SystemUpdateList = new List<ISystemUpdatable>();

			IGameSystem[] systems = Container.GetComponents<IGameSystem>();

			for (int i = 0; i < systems.Length; ++i)
			{
				SystemDictionary.Add(systems[i].GetType(), systems[i]);
				systems[i].Initialize();
			}

			UnityEngine.Object.DontDestroyOnLoad(container);
		}

		/// <summary>
		/// 新增子系統並執行子系統初始化.
		/// </summary>
		public T AddGameSystem<T>() where T : IGameSystem, new()
		{
			// 如果該子系統已經存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				EditorTool.Log("[ GetSystem ] 子系統 " + typeof(T).Name + " 已經存在.", LogType.Warning);
				return null;
			}
			else
			{
				// 建立子系統區域暫存變數.
				T system = new T();

				// 執行子系統初始化.
				system.Initialize();

				// 將子系統加入到SystemDictionary進行管理.
				SystemDictionary.Add(system.GetType(), system);

				// 判斷如果為IGameSystemUpdatable.
				if (system is ISystemUpdatable)
				{
					// 將子系統加入到SystemUpdateList進行管理.
					SystemUpdateList.Add(system as ISystemUpdatable);
				}

				return system;
			}
		}

		/// <summary>
		/// 取得子系統.
		/// </summary>
		public T GetGameSystem<T>() where T : IGameSystem
		{
			// 判斷該子系統是否存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				return SystemDictionary[typeof(T)] as T;
			}
			else
			{
				EditorTool.Log("[ GetSystem ] 子系統 " + typeof(T).Name + " 不存在.", LogType.Warning);
				return null;
			}
		}

		/// <summary>
		/// 執行System身上的Update.
		/// </summary>
		public void SystemUpdate()
		{
			for (int i = 0; i < SystemUpdateList.Count; ++i)
			{
				SystemUpdateList[i].SystemUpdate();
			}
		}

		/// <summary>
		/// 移除子系統並執行子系統釋放.
		/// </summary>
		public void RemoveGameSystem<T>() where T : IGameSystem
		{
			// 如果該子系統存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				// 執行子系統釋放.
				SystemDictionary[typeof(T)].Release();

				// 如果該子系統是IGameSystemUpdatable介面.
				if (SystemUpdateList.Contains(SystemDictionary[typeof(T)] as ISystemUpdatable))
				{
					// 從SystemUpdateList移除該子系統.
					SystemUpdateList.Remove(SystemDictionary[typeof(T)] as ISystemUpdatable);
				}

				// 移除子系統.
				SystemDictionary.Remove(typeof(T));
			}
		}

		/// <summary>
		/// 移除所有子系統.
		/// </summary>
		public void RemoveAllGameSystem()
		{
			foreach (KeyValuePair<Type, IGameSystem> system in SystemDictionary)
			{
				system.Value.Release();
			}

			SystemDictionary.Clear();
			SystemUpdateList.Clear();
		}
	}
}