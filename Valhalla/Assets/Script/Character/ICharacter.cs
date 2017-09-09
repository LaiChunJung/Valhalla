using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public abstract class ICharacter
	{
		protected GameObject CharacterObject;
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
		/// 建立角色.
		/// </summary>
		/// <param name="assetPath">角色Prefab檔案路徑.</param>
		public ICharacter (string assetPath)
		{
			CharacterObject = Object.Instantiate(Resources.Load<GameObject>(assetPath));

			if(!CharacterObject)
			{
				Debug.LogWarning("[ Iplayer ] Can't find the asset path '" + assetPath + "'.");
				return;
			}

			trans = CharacterObject.transform;
			anim = CharacterObject.GetComponent<Animator>();
			controller = CharacterObject.GetComponent<CharacterController>();
			phview = CharacterObject.GetComponent<PhotonView>();
		}

		/// <summary>
		/// 初始化角色資料.
		/// </summary>
		public virtual void Initialize()
		{

		}

		/// <summary>
		/// 釋放角色記憶體資料. (用於角色死亡 or 遊戲關閉)
		/// </summary>
		public virtual void Release()
		{
			CharacterObject = null;
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
			return CharacterObject;
		}

		/// <summary>
		/// 取得角色的Transform.
		/// </summary>
		public Transform GetTransform()
		{
			return trans;
		}

		public CharacterController GetController()
		{
			return controller;
		}

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


