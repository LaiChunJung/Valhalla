using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 角色基底虛擬類別.
	/// </summary>
	public abstract class ICharacter
	{
		private int battleID;
		protected GameObject characterObject;
		protected Transform trans;
		protected Animator anim;
		protected PhotonView phview;
		protected CharacterController controller;
		protected Collider[] Bones;
		protected Rigidbody[] Rigs;
		protected Vector3 MoveDirection = Vector3.zero;

		protected float Hp = 0.0f;
		protected static readonly float RotSpeed = 0.0f;

		protected bool isMovable = true;
		protected bool isHitable = true;

		// 存放角色各個動畫狀態的Dictionary;
		private Dictionary<string, int> AnimStates = new Dictionary<string, int>();

		/// <summary>
		/// 初始化角色資料.
		/// </summary>
		public virtual void Initialize()
		{
			if(!characterObject)
			{
				Debug.LogWarning("[ Character Initialize ] The character gameobject is null.");
				return;
			}

			trans = characterObject.transform;
			anim = characterObject.GetComponent<Animator>();
			controller = characterObject.GetComponent<CharacterController>();
			phview = characterObject.GetComponent<PhotonView>();
		}

		/// <summary>
		/// 釋放角色記憶體資料. (用於角色死亡 or 遊戲關閉)
		/// </summary>
		public virtual void Release()
		{
			characterObject = null;
			trans = null;
			anim = null;
			controller = null;
			phview = null;
			Bones = null;
			Rigs = null;
			AnimStates.Clear();
		}
		
		/// <summary>
		/// 取得角色之遊戲物件.
		/// </summary>
		public GameObject GetGameObject()
		{
			return characterObject;
		}

		/// <summary>
		/// 取得角色的Transform.
		/// </summary>
		public Transform GetTransform()
		{
			return trans;
		}

		/// <summary>
		/// 取得角色的CharacterController.
		/// </summary>
		/// <returns></returns>
		public CharacterController GetController()
		{
			return controller;
		}

		/// <summary>
		/// 取得角色的Animator.
		/// </summary>
		/// <returns></returns>
		public Animator GetAnimator()
		{
			return anim;
		}

		/// <summary>
		/// 取得當前動畫狀態.
		/// </summary>
		public AnimatorStateInfo GetCurrentStateInfo(int layer)
		{
			return anim.GetCurrentAnimatorStateInfo(layer);
		}

		/// <summary>
		/// 設定可否移動.
		/// </summary>
		public void SetMovable(bool active)
		{
			isMovable = active;
		}

		/// <summary>
		/// 是否可以移動.
		/// </summary>
		public bool IsMovable()
		{
			return isMovable;
		}
	}
}


