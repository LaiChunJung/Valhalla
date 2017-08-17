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
		public static GameObject FindGameObject(string name)
		{
			GameObject obj = GameObject.Find(name);

			if (obj)
				return obj;

			EditorTool.Log("[ FindGameObject ] Can't find the GameObject '" + name + "'.", LogType.Warning);

			return null;
		}

		/// <summary>
		/// 尋找特定物件之下的子物件.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static GameObject FindChildObject(GameObject parent, string childName)
		{
			if(!parent)
			{
				EditorTool.Log("[ FindChildObject ] Can't find the object '" + childName + "', because the parent object is null.", LogType.Warning);
				return null;
			}

			bool isFound = false;

			Transform result = null;

			Transform[] children = parent.GetComponentsInChildren<Transform>();

			for(int i = 0; i < children.Length; ++i)
			{
				if(children[i].name.Equals(childName))
				{
					if (isFound)
					{
						EditorTool.Log("[ FindChildObject ] The child object '" + childName + "' in " + parent.name + " is plural.", LogType.Warning);
					}
					result = children[i];
					isFound = true;
				}
			}

			return result.gameObject;
		}
	}
}
