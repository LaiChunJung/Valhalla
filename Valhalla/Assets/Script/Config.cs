using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public static class Config
{
	public const float gcFrequency = 60.0f;         // 記憶體回收頻率. (單位：秒)

	/// <summary>
	/// 初始化遊戲設定.
	/// </summary>
	public static void Initialize()
	{
		Application.targetFrameRate = 60;

		Timing.RunCoroutine(_RegularGC(gcFrequency));
	}

	/// <summary>
	/// 記憶體回收.
	/// </summary>
	public static void GarbageCollect()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	/// <summary>
	/// 固定時間記憶體回收.
	/// </summary>
	/// <param name="sec">秒數</param>
	/// <returns></returns>
	private static IEnumerator<float> _RegularGC(float sec)
	{
		while(true)
		{
			GarbageCollect();
			yield return Timing.WaitForSeconds(sec);
		}
	}
}
