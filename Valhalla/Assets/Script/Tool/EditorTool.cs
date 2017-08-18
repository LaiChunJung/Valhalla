using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using UnityEngine.SceneManagement;

namespace Valhalla
{
	/// <summary>
	/// 訊息類型. (Normal - 一般訊息；Warning - 警告訊息；Error - 錯誤訊息)
	/// </summary>
	public enum LogType
	{
		Normal,
		Warning,
		Error
	}

	public class EditorTool
	{
		/// <summary>
		/// 編譯器專用特化Log.
		/// </summary>
		public static void Log(string msg, LogType type)
		{
			switch(type)
			{
				case LogType.Normal:
#if UNITY_EDITOR
					Debug.Log(string.Format("{0}", msg));
#endif
					break;
				case LogType.Warning:
#if UNITY_EDITOR
					Debug.LogWarning(string.Format("{0}", msg));
#endif
					break;
				case LogType.Error:
#if UNITY_EDITOR
					Debug.LogError(string.Format("{0}", msg));
#endif
					break;
				default:
#if UNITY_EDITOR
					Debug.LogError("LogType " + type.ToString() + " is unknown.");
#endif
					break;
			}
		}
	}

}