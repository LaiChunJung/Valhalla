using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Valhalla
{
	public class CharacterSystem : ISystem, ISystemUpdatable
	{		
		private ICharacter player;						// 主要角色.
		private PlayerController playerCtrl;            // 角色控制器.
		private Dictionary<int, ICharacter> playerDic;  // 存放所有角色參考的Dictionary.

		public CharacterSystem()
		{
			playerDic = new Dictionary<int, ICharacter>();
		}

		public Vector3 StartPosition                    // 角色初始位置
		{
			get
			{
				GameObject temp;

				temp = GameTool.FindGameObject("Start Position");

				return GameObject.Find("Start Position").transform.position;
			}

			private set { }
		}

		public void Initialize()
		{
			player = CreateCharacter(new Magi(StartPosition, Quaternion.identity));

			playerCtrl = new PlayerController(player);

			PlayerCamera.Instance.active = true;
			PlayerCamera.Instance.SetTarget(player.GetTransform());
		}

		public void SystemUpdate()
		{
			playerCtrl.InputUpdate();
		}

		public void Release()
		{
			playerDic.Clear();
			player = null;
			playerCtrl = null;
		}

		/// <summary>
		/// 建立玩家角色. (未完成)
		/// </summary>
		/// <param name="fileName">資源檔案名稱.</param>
		/// <param name="position">初始位置.</param>
		/// <param name="rotation">初始角度.</param>
		/// <returns></returns>
		public ICharacter CreateCharacter(ICharacter character)
		{
			playerDic.Add(character.GetGameObject().GetInstanceID(), character);

			return character;
		}
	}
}
