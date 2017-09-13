using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public static class Math
	{
		/// <summary>
		/// 限制角度大小.
		/// </summary>
		/// <param name="angle">目標角度</param>
		/// <param name="min">最小角度</param>
		/// <param name="max">最大角度</param>
		/// <returns></returns>
		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360)
				angle += 360;
			if (angle > 360)
				angle -= 360;
			return Mathf.Clamp(angle, min, max);
		}
	}
}
