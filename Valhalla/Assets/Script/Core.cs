using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 場景中的系統核心全域靜態物件.
	/// </summary>
	public sealed class Core : IMonoSingleton<Core>
	{
		private static SystemCore m_SystemCore;
		private static bool isApplicationQuit = false;

		private void OnApplicationQuit()
		{
			m_SystemCore.RemoveAllSystem();
			isApplicationQuit = true;
		}

		public static void Init()
		{
			if (m_SystemCore != null)
			{
				Debug.Log("SystemCore has been initialized. It can not be initilaized again.");
				return;
			}

			// 初始化系統核心.
			m_SystemCore = new SystemCore(Instance.gameObject);

			// 設為不滅的全域靜態物件.
			DontDestroyOnLoad(Instance.gameObject);

			Debug.Log("SystemCore has been initialized.");
		}

		public static void AddSystem<T>() where T : class, ISystem, new()
		{
			if (!IsManagerInit() || isApplicationQuit)
				return;

			m_SystemCore.AddSystem<T>();
		}

		public static void AddSystemMono<T>() where T : MonoBehaviour, ISystem
		{
			if (!IsManagerInit() || isApplicationQuit)
				return;

			m_SystemCore.AddSystemMono<T>();
		}

		public static T GetSystem<T> () where T : class, ISystem
		{
			if (!IsManagerInit() || isApplicationQuit)
				return null;

			return m_SystemCore.GetSystem<T>();
		}

		public static void SystemUpdate()
		{
			if (!IsManagerInit() || isApplicationQuit)
				return;

			m_SystemCore.SystemUpdate();
		}

		public static void RemoveSystem<T> () where T : class, ISystem
		{
			if (!IsManagerInit() || isApplicationQuit)
				return;

			m_SystemCore.RemoveSystem<T>();
		}

		public static void RemoveAllSystem()
		{
			if (!IsManagerInit() || isApplicationQuit)
				return;

			m_SystemCore.RemoveAllSystem();
		}

		/// <summary>
		/// 檢查SystemCore是否已經初始化.
		/// </summary>
		/// <returns></returns>
		private static bool IsManagerInit()
		{
			if (m_SystemCore == null)
			{
				Debug.Log("SystemCore has not been initialized yet.");
				return false;
			}

			return true;
		}
	}
}


