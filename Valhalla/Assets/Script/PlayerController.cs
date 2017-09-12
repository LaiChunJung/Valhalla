using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 角色控制器.
	/// </summary>
	public class PlayerController : ISingleton<PlayerController>
	{
		private const string horizontal = "Valhalla Horizontal";
		private const string vertical = "Valhalla Vertical";
		private ICharacter player;
		private float Input_Horizontal
		{
			get
			{
				return Input.GetAxis(horizontal);
			}
		}
		private float Input_Vertical
		{
			get
			{
				return Input.GetAxis(vertical);
			}
		}


		public PlayerController() { }

		public PlayerController(ICharacter character)
		{
			if(character == null)
			{
				Debug.LogWarning("[ PlayerController ] The parameter 'player' is null.");
				return;
			}

			player = character;
		}

		/// <summary>
		/// 玩家控制輸入監聽.
		/// </summary>
		public void InputCtrl()
		{
			if(!CheckPlayerExists())
			{
				Debug.LogWarning("要控制的角色不存在.");
				return;
			}
		}

		/// <summary>
		/// 設定要控制的角色.
		/// </summary>
		/// <param name="_player"></param>
		public void SetTargetPlayer(ICharacter character)
		{
			player = character;
		}

		/// <summary>
		/// 檢查要控制的角色是否存在.
		/// </summary>
		/// <returns></returns>
		public bool CheckPlayerExists()
		{
			if(player == null)
			{
				return false;
			}

			return true;
		}
	}
}