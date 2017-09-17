using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 角色控制器.
	/// </summary>
	public class PlayerController : IGameSystem, ISystemUpdatable
	{
		private ICharacter player;

		public const string horizontal			= "Valhalla Horizontal";
		public const string vertical			= "Valhalla Vertical";
		public const string mouseX				= "Valhalla Mouse X";
		public const string mouseY				= "Valhalla Mouse Y";
		public const string mouseScrollWheel	= "Valhalla Mouse ScrollWheel";
		public const string jump				= "Valhalla Jump";
		public const string dodge				= "Valhalla Dodge";

		#region ------ Properties ------
		public float Input_Horizontal				// 鍵盤輸入前後方向 (W、S、↑、↓)
		{
			get
			{
				return Input.GetAxis(horizontal);
			}
			private set { }
		}
		public float Input_Vertical					// 鍵盤輸入左右方向 (A、D、←、→)
		{
			get
			{
				return Input.GetAxis(vertical);
			}
			private set { }
		}
		public float RunValue                       // 角色跑步動畫條件值
		{
			get
			{
				return Mathf.Abs(Input_Horizontal) + Mathf.Abs(Input_Vertical);
			}
			private set { }
		}
		public float Input_MouseX                   // 滑鼠座標X位置
		{
			get
			{
				return Input.GetAxis(mouseX);
			}
			private set { }
		}
		public float Input_MouseY					// 滑鼠座標Y位置
		{
			get
			{
				return Input.GetAxis(mouseY);
			}
			private set { }
		}
		public float Input_MouseScrollWheel			// 滑鼠滾輪
		{
			get
			{
				return Input.GetAxis(mouseScrollWheel);
			}
		}
		public bool Input_Jump						// 鍵盤輸入跳躍 (space)
		{
			get
			{
				return Input.GetButtonDown(jump);
			}
			private set { }
		}
		public bool Input_Dodge						// 鍵盤輸入閃避 (shift)
		{
			get
			{
				return Input.GetButtonDown(dodge);
			}
			private set { }
		}
		public Vector3 MoveDirection                // 鍵盤輸入三維方向向量
		{
			get
			{
				return new Vector3(Input_Horizontal, 0.0f, Input_Vertical);
			}

			private set { }
		}
		#endregion

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Release()
		{
			base.Release();
		}

		/// <summary>
		/// 玩家控制輸入監聽.
		/// </summary>
		public void SystemUpdate()
		{
			if(!CheckPlayerExists())
			{
				Debug.LogWarning("要控制的角色不存在.");
				return;
			}

			if(Input_Horizontal != 0 || Input_Vertical != 0)
			{
				if(player.IsMovable())
				{
					player.LerpForward(MoveDirection, 0.5f);
				}
			}

			if(Input_Jump)
			{
				player.GetAnimator().SetTrigger_RPC("Jump");
			}

			player.GetAnimator().SetFloat_RPC("Run", RunValue);
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