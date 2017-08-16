using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class IUserInterface<T> : ISingleton<IUserInterface<T>>
	{
		private GameObject m_Root;
		private CanvasGroup m_CanvasGroup;
	}
}
