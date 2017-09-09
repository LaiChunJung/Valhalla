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

			Debug.LogWarning("[ FindGameObject ] Can't find the GameObject '" + name + "'.");

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
				Debug.LogWarning("[ FindChildObject ] The parent object is null.");
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
						Debug.LogWarning("[ FindChildObject ] The child object '" + childName + "' in " + parent.name + " is plural.");
					}
					result = children[i];
					isFound = true;
				}
			}

			return result.gameObject;
		}
	}
}
