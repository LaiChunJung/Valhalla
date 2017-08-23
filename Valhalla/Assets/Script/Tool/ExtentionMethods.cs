using UnityEngine;
using UnityEditor;

namespace Valhalla
{
	public static class ExtentionMethods
	{
		/// <summary>
		/// 移除組件.
		/// </summary>
		/// <typeparam name="Component"></typeparam>
		/// <param name="obj"></param>
		public static void RemoveComponent<Component>(this GameObject obj)
		{
			Component component = obj.GetComponent<Component>();
			if (component != null)
			{
				Object.Destroy(component as Object);
			}
		}

		/// <summary>
		/// 設定父物件.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		/// <param name="isCenter"></param>
		public static void SetParent(this Transform child, Transform parent, bool isCenter)
		{
			child.parent = parent.transform;

			if (!isCenter)
				return;

			child.localPosition = Vector3.zero;
			child.localRotation = Quaternion.identity;
			child.localScale = Vector3.one;
		}

		public static void SetUIParent(this RectTransform child, RectTransform parent, bool isCenter)
		{
			child.parent = parent;

			if (!isCenter)
				return;

			child.anchoredPosition = Vector2.zero;
			child.eulerAngles = Vector3.zero;
			child.localScale = Vector3.one;
		}
	}
}