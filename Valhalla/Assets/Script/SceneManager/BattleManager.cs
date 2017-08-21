using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class BattleManager : IMonoSingleton<BattleManager>
	{
		private GameSystemManager m_SystemManager;

		private void Awake()
		{
			m_SystemManager = new GameSystemManager(gameObject);

			m_SystemManager.AddGameSystem<InputSystem>();
			m_SystemManager.AddGameSystem<AnimationSystem>();
		}

		private void OnDestroy()
		{
			m_SystemManager.RemoveAllGameSystem();
		}
	}
}
