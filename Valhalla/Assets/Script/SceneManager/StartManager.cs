using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class StartManager : IMonoSingleton<StartManager>
	{
		GameSystemManager m_SystemManager;

		private void Awake()
		{
			m_SystemManager = new GameSystemManager(gameObject);

			InitUI();
		}

		private void Start()
		{
			
		}

		private void OnDestroy()
		{
			m_SystemManager.RemoveAllSystem();
		}

		private void InitUI()
		{
			UITool.BuildUICanvas("StartCanvas");

			m_SystemManager.AddSystem<LogoUI>();
		}

		private T GetSystem<T> () where T : class, ISystem
		{
			return m_SystemManager.GetSystem<T>();
		}
	}
}
