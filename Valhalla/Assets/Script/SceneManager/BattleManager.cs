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
		private CharacterSystem m_CharacterSystem
		{
			get
			{
				return Core.GetSystem<CharacterSystem>();
			}
		}
		private InputSystem m_InputSystem
		{
			get
			{
				return Core.GetSystem<InputSystem>();
			}
		}
		private PlayerController m_PlayerController
		{
			get
			{
				return Core.GetSystem<PlayerController>();
			}
		}

		// 角色初始位置.
		public Vector3 StartPosition
		{
			get
			{
				GameObject temp;

				temp = GameTool.FindGameObject("Start Position");

				return GameObject.Find("Start Position").transform.position;
			}

			private set { }
		}

		private void Awake()
		{
			Core.Init();

			UITool.BuildUICanvas("BattleCanvas");

			InitSystem();
		}

		private void Start()
		{
			// 定義何種角色.
			ICharacter player = new Magi(StartPosition, Quaternion.identity);

			// 建立角色.
			m_CharacterSystem.CreateCharacter(player);

			// 設定角色控制器要控制的角色.
			m_PlayerController.SetTargetPlayer(player);

			// 設定攝影機要觀看的目標角色.
			PlayerCamera.Instance.SetTarget(player);
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
			
			Core.AddSystem<AnimationSystem>();
			Core.AddSystem<CharacterSystem>();
			Core.AddSystem<InputSystem>();
			Core.AddSystem<CameraSystem>();
			Core.AddSystem<PlayerController>();
		}

		// 釋放系統.
		private void ReleaseSystem()
		{
			Core.RemoveSystem<LoadingUI>();
			
			Core.RemoveSystem<AnimationSystem>();
			Core.RemoveSystem<CharacterSystem>();
			Core.RemoveSystem<InputSystem>();
			Core.RemoveSystem<CameraSystem>();
			Core.RemoveSystem<PlayerController>();
		}
	}
}
