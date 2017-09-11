using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace Valhalla
{
	/// <summary>
	/// 場景中的系統核心全域靜態物件.
	/// </summary>
	public sealed class Core : IMonoSingleton<Core>
	{
		private static SystemCore m_SystemCore;
		private static bool isInitialized = false;
		private static bool isApplicationQuit = false;

		private void Awake()
		{
			Config.Initialize();
		}

		private void OnApplicationQuit()
		{
			m_SystemCore.RemoveAllSystem();
			isApplicationQuit = true;
		}

		/// <summary>
		/// 初始化系統核心.
		/// </summary>
		public static void Init()
		{
			if (m_SystemCore != null)
			{
				Debug.Log("SystemCore has been initialized. It can not be initilaized again.");
				return;
			}
			
			m_SystemCore = new SystemCore(Instance.gameObject);

			// 設為不滅的全域靜態物件.
			DontDestroyOnLoad(Instance.gameObject);

			isInitialized = true;

			Debug.Log("SystemCore has been initialized.");
		}

		/// <summary>
		/// 新增系統.
		/// </summary>
		/// <typeparam name="T">T必須繼承於ISystem介面，且必須實作一個無任何引數的建構子.</typeparam>
		public static void AddSystem<T>() where T : class, ISystem, new()
		{
			if (!CheckCoreIsEnabled())
				return;

			m_SystemCore.AddSystem<T>();
		}

		/// <summary>
		/// 新增MonoBehaviour系統.
		/// </summary>
		/// <typeparam name="T">T必須繼承於MonoBehaviour類別與ISystem介面.</typeparam>
		public static void AddSystemMono<T>() where T : MonoBehaviour, ISystem
		{
			if (!CheckCoreIsEnabled())
				return;

			m_SystemCore.AddSystemMono<T>();
		}

		/// <summary>
		/// 取得系統.
		/// </summary>
		/// <typeparam name="T">T必須繼承於ISystem介面，且必須為class.</typeparam>
		/// <returns>回傳T.</returns>
		public static T GetSystem<T> () where T : class, ISystem
		{
			if (!CheckCoreIsEnabled())
				return null;

			return m_SystemCore.GetSystem<T>();
		}

		/// <summary>
		/// 系統Update監聽.
		/// </summary>
		public static void SystemUpdate()
		{
			if (!CheckCoreIsEnabled())
				return;

			m_SystemCore.SystemUpdate();
		}

		/// <summary>
		/// 移除系統.
		/// </summary>
		/// <typeparam name="T">T必須繼承於ISystem，且必須為class.</typeparam>
		public static void RemoveSystem<T> () where T : class, ISystem
		{
			if(!CheckCoreIsEnabled())
				return;

			m_SystemCore.RemoveSystem<T>();
		}

		/// <summary>
		/// 移除所有系統.
		/// </summary>
		public static void RemoveAllSystem()
		{
			if(!CheckCoreIsEnabled())
				return;

			m_SystemCore.RemoveAllSystem();
		}

		/// <summary>
		/// 檢查系統核心是否可以運作.
		/// </summary>
		/// <returns></returns>
		public static bool CheckCoreIsEnabled()
		{
			if (!isInitialized || isApplicationQuit)
				return false;

			return true;
		}
	}
}


