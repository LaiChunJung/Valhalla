using System;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 系統核心.
	/// </summary>
	public sealed class SystemCore 
	{
		private GameObject Container;
		private Dictionary<Type, ISystem> SystemDictionary;
		private List<ISystemUpdatable> SystemUpdateList;

		private SystemCore() { }

		/// <summary>
		/// 建構子，初始化管理器.
		/// </summary>
		public SystemCore(GameObject container)
		{
			Container = container;

			SystemDictionary = new Dictionary<Type, ISystem>();

			SystemUpdateList = new List<ISystemUpdatable>();

			ISystem[] systems = Container.GetComponents<ISystem>();

			for (int i = 0; i < systems.Length; ++i)
			{
				SystemDictionary.Add(systems[i].GetType(), systems[i]);
				Debug.Log("[ SystemCore ] System '" + systems[i].GetType().Name + "' is added.");
				systems[i].Initialize();

				if (systems[i] is ISystemUpdatable)
				{
					SystemUpdateList.Add(systems[i] as ISystemUpdatable);
				}
			}
		}

		/// <summary>
		/// 新增子系統並執行子系統初始化.
		/// </summary>
		public T AddSystem<T>() where T : class, ISystem, new()
		{
			// 如果該子系統已經存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				Debug.Log("[ AddSystem ] System '" + typeof(T).Name + "' already exists.");
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

				// 判斷如果為ISystemUpdatable.
				if (system is ISystemUpdatable)
				{
					// 將子系統加入到SystemUpdateList進行管理.
					SystemUpdateList.Add(system as ISystemUpdatable);
				}

				Debug.Log("[ AddSystem ] Add system '" + typeof(T).Name + "'.");

				return system;
			}
		}

		/// <summary>
		/// 新增MonoBehaviour子系統並執行子系統初始化.
		/// </summary>
		public T AddSystemMono<T> () where T : MonoBehaviour, ISystem
		{
			// 如果該子系統已經存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				Debug.Log("[ AddSystemMono ] Mono system '" + typeof(T).Name + "' already exists.");
				return null;
			}
			else
			{
				// 建立子系統區域暫存變數.
				T system = Container.AddComponent<T>();

				// 執行子系統初始化.
				system.Initialize();

				// 將子系統加入到SystemDictionary進行管理.
				SystemDictionary.Add(system.GetType(), system);

				// 判斷如果為ISystemUpdatable.
				if (system is ISystemUpdatable)
				{
					// 將子系統加入到SystemUpdateList進行管理.
					SystemUpdateList.Add(system as ISystemUpdatable);
				}

				Debug.Log("[ AddSystemMono ] Add mono system '" + typeof(T).Name + "'.");

				return system;
			}
		}

		/// <summary>
		/// 取得子系統.
		/// </summary>
		public T GetSystem<T>() where T : class, ISystem
		{
			// 判斷該子系統是否存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				return SystemDictionary[typeof(T)] as T;
			}

			Debug.Log("[ GetSystem ] System '" + typeof(T).Name + "' does not exist.");
			return null;
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
		public void RemoveSystem<T>() where T : class, ISystem
		{
			// 如果該子系統存在.
			if (SystemDictionary.ContainsKey(typeof(T)))
			{
				// 執行子系統釋放.
				SystemDictionary[typeof(T)].Release();

				// 如果該子系統是ISystemUpdatable介面.
				if (SystemUpdateList.Contains(SystemDictionary[typeof(T)] as ISystemUpdatable))
				{
					// 從SystemUpdateList移除該子系統.
					SystemUpdateList.Remove(SystemDictionary[typeof(T)] as ISystemUpdatable);
				}

				// 判斷是否為Unity內建的Component.
				if (SystemDictionary[typeof(T)] is Component)
				{
					Container.RemoveComponent<T>();
				}

				// 移除子系統.
				SystemDictionary.Remove(typeof(T));

				Debug.Log("[ RemoveSystem ] Remove system '" + typeof(T).Name + "'.");
			}
			else
				Debug.Log("[ RemoveSystem ] System '" + typeof(T).Name + "' does not exist.");
		}

		/// <summary>
		/// 移除所有子系統.
		/// </summary>
		public void RemoveAllSystem()
		{
			if (SystemDictionary.Count == 0)
			{
				Debug.Log("[ RemoveAllSystem ] There is not any system can be removed.");
				return;
			}

			foreach (KeyValuePair<Type, ISystem> system in SystemDictionary)
			{
				if(system.Value is Component)
				{
					UnityEngine.Object.Destroy((Component)system.Value);
				}
				system.Value.Release();
				
				Debug.Log("[ RemoveAllSystem ] Remove system '" + system.Value.GetType().Name + "'.");
			}

			SystemDictionary.Clear();
			SystemUpdateList.Clear();
		}
	}
}