using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class GameTool
	{
		/// <summary>
		/// 尋找特定物件.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static GameObject FindGameObject (string name)
		{
			GameObject obj = GameObject.Find(name);

			if (obj)
				return obj;

			EditorTool.Log("[ FindGameObject ] Can't find the GameObject '" + name + "'.", LogType.Warning);

			return null;
		}
	}
}
