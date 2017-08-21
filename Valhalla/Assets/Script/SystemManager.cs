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

		public static void AddGameSystem<T>() where T : IGameSystem, new()
		{
			if (!Instance.IsSystemManagerInit())
				return;

			m_SystemManager.AddGameSystem<T>();
		}

		public static T GetGameSystem<T> () where T : IGameSystem
		{
			if (!Instance.IsSystemManagerInit())
				return null;

			return m_SystemManager.GetGameSystem<T>();
		}

		public static void UpdateGameSystem()
		{
			if (!Instance.IsSystemManagerInit())
				return;

			m_SystemManager.SystemUpdate();
		}

		public static void RemoveGameSystem<T> () where T : IGameSystem
		{
			if (!Instance.IsSystemManagerInit())
				return;

			m_SystemManager.RemoveGameSystem<T>();
		}

		public static void RemoveAllGameSystem()
		{
			if (!Instance.IsSystemManagerInit())
				return;

			m_SystemManager.RemoveAllGameSystem();
		}

		/// <summary>
		/// 檢查SystemManager是否已經初始化.
		/// </summary>
		/// <returns></returns>
		private bool IsSystemManagerInit()
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


