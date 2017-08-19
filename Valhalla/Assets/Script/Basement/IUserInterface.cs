using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Valhalla
{
	public class IUserInterface<T> : ISingleton<IUserInterface<T>>
	{
		private GameObject m_Root;
		private CanvasGroup m_CanvasGroup;

		public IUserInterface() { }

		public IUserInterface (GameObject root)
		{
			m_Root = root;
		}
		
		public virtual void Initialize()
		{
			
		}

		public virtual void Release()
		{

		}

		#region --- Get Function ---

		/// <summary>
		/// 取得UI最外層父物件.
		/// </summary>
		/// <returns></returns>
		public GameObject GetRootObject()
		{
			return m_Root;
		}


<<<<<<< HEAD:Valhalla/Assets/Scripts/Basement/IUserInterface.cs
=======
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
>>>>>>> 168a69dc7951d561880e7f5a42ab37854f084fbc:Valhalla/Assets/Script/Basement/IUserInterface.cs
	}
}
