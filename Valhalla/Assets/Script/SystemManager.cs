using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class SystemManager : IMonoSingleton<SystemManager>
	{
		private static GameSystemManager m_SystemManager;

		public static void Init()
		{
			m_SystemManager = new GameSystemManager(Instance.gameObject);
		}

		public static void AddSystem<T>() where T : class, ISystem, new()
		{
			if (!Instance.ISInitialized())
				return;

			m_SystemManager.AddSystem<T>();
		}

		public static T GetSystem<T> () where T : class, ISystem
		{
			if (!Instance.ISInitialized())
				return null;

			return m_SystemManager.GetSystem<T>();
		}

		public static void UpdateSystem()
		{
			if (!Instance.ISInitialized())
				return;

			m_SystemManager.SystemUpdate();
		}

		public static void RemoveSystem<T> () where T : class, ISystem
		{
			if (!Instance.ISInitialized())
				return;

			m_SystemManager.RemoveSystem<T>();
		}

		public static void RemoveAllSystem ()
		{
			if (!Instance.ISInitialized())
				return;

			m_SystemManager.RemoveAllSystem();
		}

		private bool ISInitialized()
		{
			if (m_SystemManager == null)
			{
				EditorTool.Log("[ AddSystem ] SystemManager has not been initialized yet.", LogType.Warning);
				return false;
			}

			return true;
		}
	}
}


