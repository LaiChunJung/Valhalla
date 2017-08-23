using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public sealed class MainManager : IMonoSingleton<MainManager>
	{
		private static SystemManager m_SystemManager;

		private void OnApplicationQuit()
		{
			m_SystemManager.RemoveAllSystem();
		}

		public static void Init()
		{
			m_SystemManager = new SystemManager(Instance.gameObject);
		}

		public static void AddSystem<T>() where T : class, ISystem, new()
		{
			if (!Instance.IsManagerInit())
				return;

			m_SystemManager.AddSystem<T>();
		}

		public static void AddSystemMono<T>() where T : MonoBehaviour, ISystem
		{
			if (!Instance.IsManagerInit())
				return;

			m_SystemManager.AddSystemMono<T>();
		}

		public static T GetSystem<T> () where T : class, ISystem
		{
			if (!Instance.IsManagerInit())
				return null;

			return m_SystemManager.GetSystem<T>();
		}

		public static void SystemUpdate()
		{
			if (!Instance.IsManagerInit())
				return;

			m_SystemManager.SystemUpdate();
		}

		public static void RemoveSystem<T> () where T : class, ISystem
		{
			if (!Instance.IsManagerInit())
				return;

			m_SystemManager.RemoveSystem<T>();
		}

		public static void RemoveAllSystem()
		{
			if (!Instance.IsManagerInit())
				return;

			m_SystemManager.RemoveAllSystem();
		}

		/// <summary>
		/// 檢查SystemManager是否已經初始化.
		/// </summary>
		/// <returns></returns>
		private bool IsManagerInit()
		{
			if (m_SystemManager == null)
			{
				EditorTool.Log("SystemManager has not been initialized yet.", LogType.Warning);
				return false;
			}

			return true;
		}
	}
}


