using System;
using UnityEngine;

public static class Config
{
	public const float GCFrequency = 30.0f;			// 記憶體回收頻率.
	
	public static void Initialize()
	{
		Application.targetFrameRate = 60;
	}
}
