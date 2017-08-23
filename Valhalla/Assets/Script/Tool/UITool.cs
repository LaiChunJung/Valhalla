using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class UITool
	{
		public static GameObject CurrentCanvas;

		/// <summary>
		/// 建立UICanvas，參數代入Canvas的Prefab檔案名稱.
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
				EditorTool.Log("[ FindUIObject ] CurrentCanvas is not initialized.", LogType.Warning);
				return null;
			}

			Transform result = null;

			Transform[] children = CurrentCanvas.transform.GetComponentsInChildren<Transform>(true);

			bool isFound = false;

			for(int i = 0; i < children.Length; ++i)
			{
				if(children[i].name.Equals(objectName))
				{
					if (isFound)
					{
						EditorTool.Log("[ FindUIObject ] The UI object '" + objectName + "' is plural.", LogType.Warning);
						continue;
					}
					result = children[i];
					isFound = true;
				}
			}

			if(!result)
			{
				EditorTool.Log("[ FindUIObject ] Can't find the ui object '" + objectName + "'.", LogType.Warning);
				return null;
			}

			return result.gameObject;
		}

		/// <summary>
		/// 取得UI子物件的Component.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetChildUIComponent<T>(GameObject parent, string childName) where T : Object
		{
			GameObject childObj = GameTool.FindChildObject(parent, childName);

			if (!childObj)
				return null;

			T component = childObj.GetComponent<T>();

			if (!component)
			{
				EditorTool.Log("[ GetChildUIComponent ] " + childName + " is not " + typeof(T).Name + ".", LogType.Warning);
				return null;
			}

			return component;
		}

		/// <summary>
		/// 取得UI物件的Component.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="uiName"></param>
		/// <returns></returns>
		public static T GetUIComponent<T>(string uiName) where T : Object
		{
			if(!CurrentCanvas)
			{
				EditorTool.Log("[ GetUIComponent ] CurrentCanvas is null.", LogType.Warning);
				return null;
			}

			GameObject childObj = GameTool.FindGameObject(uiName);

			if (!childObj)
				return null;

			T component = childObj.GetComponent<T>();

			if (!component)
			{
				EditorTool.Log("[ GetUIComponent ] " + uiName + " is not " + typeof(T).ToString() + ".", LogType.Warning);
				return null;
			}

			return component;
		}

		/// <summary>
		/// 新增UI資源至場景.
		/// </summary>
		/// <param name="uiName"></param>
		public static void AddUIAsset(string uiName)
		{
			/*if(FindUIObject(uiName))
			{
				EditorTool.Log("[ AddUIAsset ] The UI '" + uiName + "' already exists.", LogType.Warning);
				return;
			}*/

			GameObject loadingUI = Object.Instantiate( Resources.Load<GameObject>(string.Format("{0}{1}", AssetPath.UI, uiName)) );

			loadingUI.GetComponent<RectTransform>().SetParent(CurrentCanvas.GetComponent<RectTransform>(), false);

			loadingUI.name = uiName;

			EditorTool.Log("[ AddUIAsset ] Add UI asset '" + uiName + "'.", LogType.Normal);
		}
	}
}
