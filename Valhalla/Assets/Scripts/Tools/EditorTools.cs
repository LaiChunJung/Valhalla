using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{

	public enum LogType
	{
		Normal,
		Warning,
		Error
	}

	public static class EditorTools
	{
		/// <summary>
		/// 編譯器專用特化Log.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="type"></param>
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