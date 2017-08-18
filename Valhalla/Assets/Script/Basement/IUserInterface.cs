using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Valhalla
{
	public class IUserInterface<T> : ISystem
	{
		private GameObject m_Root;
		private CanvasGroup m_CanvasGroup;
		private bool enable = true;
		private bool visible = true;

		public IUserInterface()
		{
			m_Root = UITool.FindUIObject(typeof(T).Name);
		}

		public IUserInterface(GameObject root)
		{
			m_Root = root;
		}

		public IUserInterface(string rootName)
		{
			m_Root = UITool.FindUIObject(rootName);
		}

		public virtual void Initialize() { }

		public virtual void Release() { }

		#region --- Get Function ---

		/// <summary>
		/// 取得UI最外層父物件.
		/// </summary>
		/// <returns></returns>
		public GameObject GetRootObject()
		{
			return m_Root;
		}

		/// <summary>
		/// 取得最外層父物件身上的CanvasGroup.
		/// </summary>
		/// <returns></returns>
		public CanvasGroup GetCanvasGroup()
		{
			if (m_CanvasGroup)
				return m_CanvasGroup;

			m_CanvasGroup = m_Root.GetComponent<CanvasGroup>();

			if (m_CanvasGroup)
				return m_CanvasGroup;

			return null;
		}

		/// <summary>
		/// 取得子物件身上的Component.
		/// </summary>
		/// <typeparam name="C"></typeparam>
		/// <param name="childName"></param>
		/// <returns></returns>
		public C GetChildUIComponent<C>(string childName) where C : Object
		{
			C component = UITool.GetChildUIComponent<C>(m_Root, childName);

			return component;
		}

		#endregion

		#region --- Set Function ---

		/// <summary>
		/// 顯示UI. (isFade判斷是否淡入淡出)
		/// </summary>
		/// <param name="duration"></param>
		/// <param name="isFade"></param>
		public void Show(float duration, bool isFade = false)
		{
			if (enable && !visible)
			{
				ShowSecure();
				if (isFade && GetCanvasGroup())
				{
					m_CanvasGroup.alpha = 0.0f;
					m_CanvasGroup.DOFade(1.0f, duration).OnComplete(ShowSecure);
				}
			}
		}

		/// <summary>
		/// 隱藏UI. (isFade判斷是否淡入淡出)
		/// </summary>
		/// <param name="duration"></param>
		/// <param name="isFade"></param>
		public void Hide(float duration, bool isFade = false)
		{
			if(enable && visible)
			{
				if (isFade && GetCanvasGroup())
				{
					m_CanvasGroup.alpha = 1.0f;
					m_CanvasGroup.DOFade(0.0f, duration);
				}
				else
					HideSecure();
			}
		}

		private void ShowSecure()
		{
			if (enable)
			{
				visible = true;
				m_Root.SetActive(true);
			}
		}

		private void HideSecure()
		{
			if (enable)
			{
				visible = false;
				m_Root.SetActive(false);
			}
		}

		#endregion
	}
}
