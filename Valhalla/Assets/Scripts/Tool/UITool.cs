using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class UITool
	{
		public static GameObject CurrentCanvas;

		/// <summary>
		/// 建立UICanvas.
		/// </summary>
		/// <param name="canvasName"></param>
		public static void BuildUICanvas (string canvasName)
		{
			string path = string.Format("{0}{1}", AssetPath.UI, canvasName);

			GameObject prefab = Resources.Load<GameObject>(path);

			if (!prefab)
			{
				EditorTool.Log("[ BuildUICanvas ] Can't find the canvas prefab 'Resources/" + path + "'.", LogType.Warning);
				return;
			}

			GameObject canvasObj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

			CurrentCanvas = canvasObj;

			canvasObj.name = canvasName;
		}

		/// <summary>
		/// 取得特定UI物件.
		/// </summary>
		/// <param name="objectName"></param>
		/// <returns></returns>
		public static GameObject FindUIObject(string objectName)
		{
			if(!CurrentCanvas)
			{
				EditorTool.Log("[ FindUIObject ] UITool.CurrentCanvas is not initialized.", LogType.Warning);
				return null;
			}

			Transform[] children = CurrentCanvas.transform.GetComponentsInChildren<Transform>();
			bool isFinded = false;
			for(int i = 0; i < children.Length; ++i)
			{
				if(children[i].name.Equals(objectName))
				{
					if (isFinded)
					{
						EditorTool.Log("[ FindUIObject ] The UI object '" + objectName + "' is plural.", LogType.Warning);
						continue;
					}

					isFinded = true;
				}
			}
			return children[0].gameObject;
		}
	}
}
