using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using UnityEngine.SceneManagement;

namespace Valhalla
{
	/// <summary>
	/// 編譯器專用特化Debug.
	/// </summary>
	public class Debug
	{
		/// <summary>
		/// 印出訊息.
		/// </summary>
		/// <param name="msg"></param>
		public static void Log(params string[] msg)
		{
#if UNITY_EDITOR
			UnityEngine.Debug.Log(string.Format("{0}", msg));
#endif			
		}

		/// <summary>
		/// 印出警告訊息.
		/// </summary>
		/// <param name="msg"></param>
		public static void LogWarning(params string[] msg)
		{
#if UNITY_EDITOR
			UnityEngine.Debug.LogWarning(string.Format("{0}", msg));
#endif
		}

		/// <summary>
		/// 印出錯誤訊息.
		/// </summary>
		/// <param name="msg"></param>
		public static void LogError(params string[] msg)
		{
#if UNITY_EDITOR
			UnityEngine.Debug.LogError(string.Format("{0}", msg));
#endif
		}
	}
}