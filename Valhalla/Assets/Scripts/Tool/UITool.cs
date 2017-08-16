using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class UITool
	{
		public static void BuildUICanvas (string canvasName)
		{
			string path = string.Format("{0}{1}", AssetPath.UI, canvasName);

			GameObject canvasObj = Object.Instantiate(Resources.Load(path) as GameObject, Vector3.zero, Quaternion.identity);
		}
	}
}
