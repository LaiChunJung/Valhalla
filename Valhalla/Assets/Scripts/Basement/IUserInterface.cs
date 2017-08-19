using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		/// <summary>
		/// 取得UI最外層父物件.
		/// </summary>
		/// <returns></returns>
		public GameObject GetRootObject()
		{
			return m_Root;
		}


	}
}
