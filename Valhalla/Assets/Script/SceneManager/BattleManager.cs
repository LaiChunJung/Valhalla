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
			Core.Init();

			UITool.BuildUICanvas("BattleCanvas");

			InitSystem();
		}

		private void Update()
		{
			Core.SystemUpdate();
		}

		private void OnDestroy()
		{
			ReleaseSystem();
		}

		// 新增系統.
		private void InitSystem()
		{
			Core.AddSystem<LoadingUI>();

			Core.AddSystem<InputSystem>();
			Core.AddSystem<AnimationSystem>();
			Core.AddSystem<CharacterSystem>();
			Core.AddSystem<CameraSystem>();
		}

		// 釋放系統.
		private void ReleaseSystem()
		{
			Core.RemoveSystem<LoadingUI>();

			Core.RemoveSystem<InputSystem>();
			Core.RemoveSystem<AnimationSystem>();
			Core.RemoveSystem<CharacterSystem>();
			Core.RemoveSystem<CameraSystem>();
		}
	}
}
