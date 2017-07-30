using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class Main : MonoBehaviour
	{
		GameSystemManager m_SystemManager;

		private void Awake()
		{
			m_SystemManager = new GameSystemManager(this.gameObject);
		}
	}
}


