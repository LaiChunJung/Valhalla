using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Valhalla
{
	public class IUserInterface<T> : ISystem
	{
		protected GameObject m_Root;
		protected CanvasGroup m_CanvasGroup;

		public IUserInterface()
		{
			m_Root = UITool.FindUIObject(typeof(T).Name);
		}

		public IUserInterface (GameObject root)
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

		public void Show (bool isFade = false)
		{
			
		}

		public void SetUIVisible(string uiName, bool visible, bool isFade = false)
		{

		}

		#endregion
	}
}
