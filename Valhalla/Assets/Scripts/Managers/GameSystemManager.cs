using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/******************************************************
	
	系統管理器，建構子參數代入場景上一物件(GameObject)作為容器.

	******************************************************/
	public sealed class GameSystemManager
	{
		private GameObject container;
		private Dictionary<Type, IGameSystem> SystemDictionary;
		private List<IGameSystemUpdatable> SystemUpdateList;

		/// <summary>
		/// 建構子，初始化管理器，將管理器物件設為DontDestroyOnLoad.
		/// </summary>
		public GameSystemManager(GameObject _container)
		{
			container = _container;

			SystemDictionary = new Dictionary<Type, IGameSystem>();

			SystemUpdateList = new List<IGameSystemUpdatable>();

			UnityEngine.Object.DontDestroyOnLoad(container);
		}

		/// <summary>
		/// 新增子系統並執行子系統初始化.
		/// </summary>
		public T AddSystem<T>() where T : class, IGameSystem, new()
		{
			// 如果該子系統已經存在.
			if(SystemDictionary.ContainsKey(typeof(T)))
			{
				EditorTools.Log("[ GetSystem ] 子系統 " + typeof(T).Name + " 已經存在.", LogType.Warning);
				return null;
			}
			else
			{
				// 建立子系統區域暫存變數.
				T _system = new T();

				// 執行子系統初始化.
				_system.Initialize();

				// 將子系統加入到SystemDictionary進行管理.
				SystemDictionary.Add(_system.GetType(), _system);

				// 判斷如果為IGameSystemUpdatable.
				if (_system is IGameSystemUpdatable)
				{
					// 將子系統加入到SystemUpdateList進行管理.
					SystemUpdateList.Add(_system as IGameSystemUpdatable);
				}

				return _system;
			}
		}

		/// <summary>
		/// 移除子系統並執行子系統釋放.
		/// </summary>
		public void RemoveSystem<T>() where T : class, IGameSystem
		{
			// 如果該子系統存在.
			if(SystemDictionary.ContainsKey(typeof(T)))
			{
				// 執行子系統釋放.
				SystemDictionary[typeof(T)].Release();

				// 如果該子系統是IGameSystemUpdatable介面.
				if (SystemUpdateList.Contains(SystemDictionary[typeof(T)] as IGameSystemUpdatable))
				{
					// 從SystemUpdateList移除該子系統.
					SystemUpdateList.Remove(SystemDictionary[typeof(T)] as IGameSystemUpdatable);
				}

				// 移除子系統.
				SystemDictionary.Remove(typeof(T));
			}
		}

		/// <summary>
		/// 取得子系統.
		/// </summary>
		public T GetSystem<T>() where T : class, IGameSystem
		{
			// 判斷該子系統是否存在.
			if(SystemDictionary.ContainsKey(typeof(T)))
			{
				return SystemDictionary[typeof(T)] as T;
			}
			else
			{
				EditorTools.Log("[ GetSystem ] 子系統 " + typeof(T).Name + " 不存在.", LogType.Warning);
				return null;
			}
		}
	}

}