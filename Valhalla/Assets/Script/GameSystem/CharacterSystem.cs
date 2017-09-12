using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Valhalla
{
	public class CharacterSystem : ISystem, ISystemUpdatable
	{
		public Vector3 StartPosition					// 角色初始位置.
		{
			get
			{
				GameObject temp;

				temp = GameObject.Find("Start Position");

				if(!temp)
				{
					Debug.LogWarning("[ StartPosition ] Can't find the gameobject 'StartPosition'.");

					return Vector3.zero;
				}

				return GameObject.Find("Start Position").transform.position;
			}

			private set { }
		}

		private ICharacter player;						// 主要角色.
		private PlayerController playerCtrl;			// 角色控制器.


		public CharacterSystem() { }

		public void Initialize()
		{
			player = new Magi(StartPosition, Quaternion.identity);

			playerCtrl = new PlayerController(player);

			PlayerCamera.Instance.active = true;
			PlayerCamera.Instance.SetTarget(player.GetTransform());
		}

		public void SystemUpdate()
		{
			playerCtrl.InputCtrl();
		}

		public void Release()
		{
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
			return character;
		}
	}
}
