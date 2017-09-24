using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Valhalla
{
	/// <summary>
	/// UI的透明度狀態.
	/// </summary>
	public enum VisualState
	{
		Visible,
		Fading,
		Unvisible
	}

	/// <summary>
	/// UI基底類別.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IUserInterface<T> : ISystem
	{
		protected GameObject m_Root;
		protected CanvasGroup m_CanvasGroup;
		private bool enabled = true;
		private VisualState state = VisualState.Visible;

		/// <summary>
		/// 存取.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return enabled;
			}

			set
			{
				enabled = value;
			}
		}

		/// <summary>
		/// 自動搜尋UI物件. (以自身類別名稱進行搜尋)
		/// </summary>
		public IUserInterface()
		{
			m_Root = UITool.FindUIObject(typeof(T).Name);
		}

		/// <summary>
		/// 代入指定UI物件.
		/// </summary>
		/// <param name="root"></param>
		public IUserInterface (GameObject root)
		{
			m_Root = root;
		}

		/// <summary>
		/// 代入欲搜尋的UI名稱.
		/// </summary>
		/// <param name="rootName"></param>
		public IUserInterface(string rootName)
		{
			m_Root = UITool.FindUIObject(rootName);
		}

		public virtual void Initialize() { }

		public virtual void Release() { }

		#region --- Get ---
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
		public C GetChildUIComponent<C>(string childName)where C : Object
		{
			C component = UITool.GetChildUIComponent<C>(m_Root, childName);

			return component;
		}
		#endregion
		
		#region --- Set ---
		public void Show (bool isFade = false, float duration = 1.5f, TweenCallback onComplete = null)
		{
			if (state == VisualState.Unvisible && enabled)
			{
				onComplete += () =>
				{
					state = VisualState.Visible;
				};

				if (isFade)
				{
					// 淡入為可視狀態.
					m_CanvasGroup.DOFade(1.0f, duration).OnComplete(onComplete);
					state = VisualState.Fading;
				}
				else
				{
					// 瞬間切換為可視狀態.
					m_CanvasGroup.alpha = 1.0f;
					onComplete();
				}
			}
		}

		public void Hide(bool isFade = false, float duration = 1.5f, TweenCallback onComplete = null)
		{
			if (state == VisualState.Visible && enabled)
			{
				onComplete += () => 
				{
					state = VisualState.Unvisible;
				};

				if (isFade)
				{
					// 淡入為不可視狀態.
					m_CanvasGroup.DOFade(0.0f, duration).OnComplete(onComplete);
					state = VisualState.Fading;
				}
				else
				{
					// 瞬間切換為不可視狀態.
					m_CanvasGroup.alpha = 0.0f;
					onComplete();
				}
			}
		}
		#endregion
	}
}
