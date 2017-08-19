using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class StartManager : IMonoSingleton<StartManager>
	{
		GameSystemManager m_SystemManager;
		LogoUI m_LogoUI;

		private void Awake()
		{
			m_SystemManager = new GameSystemManager(gameObject);

			UITool.BuildUICanvas("StartCanvas");

			//m_LogoUI = new LogoUI(UITool.FindUIObject);
		}

		private void Start()
		{
			
		}
	}
}
