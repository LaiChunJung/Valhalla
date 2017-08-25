using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------
 * 
 * 戰鬥場景管理器.
 *	-2017/8/26 by Mahua.
 * 
 * ------------------------------*/

namespace Valhalla
{
	public class BattleManager : IMonoSingleton<BattleManager>
	{
		private void Awake()
		{
			MainManager.Init();

			UITool.BuildUICanvas("BattleCanvas");

			InitSystem();
		}

		private void Update()
		{
			MainManager.SystemUpdate();
		}

		// 新增系統.
		private void InitSystem()
		{
			MainManager.AddSystem<LoadingUI>();

			MainManager.AddSystem<InputSystem>();
			MainManager.AddSystem<AnimationSystem>();
		}

		private void ReleaseSystem()
		{
			MainManager.RemoveSystem<LoadingUI>();

			MainManager.RemoveSystem<InputSystem>();
			MainManager.RemoveSystem<AnimationSystem>();
		}
	}
}
