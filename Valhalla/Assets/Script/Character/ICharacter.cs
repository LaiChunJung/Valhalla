using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public abstract class ICharacter
	{
		protected GameObject CharacterObject;

		private float Hp					= 0.0f;
		protected const float RotSpeed		= 0.0f;

		protected Vector3 MoveDirection = Vector3.zero;

		protected bool movable = true;
		protected bool hitable = true;

		protected Transform				Trans;
		protected Animator				Anim;
		protected PhotonView			Phview;
		protected CharacterController	Controller;

		protected Collider[]	Bones;
		protected Rigidbody[]	Rigs;

		// 存放角色各個動畫狀態的Dictionary;
		private Dictionary<string, int> AnimStates = new Dictionary<string, int>();

		/// <summary>
		/// 建立角色.
		/// </summary>
		/// <param name="asset"></param>
		public ICharacter (string AssetPath)
		{
			CharacterObject = Object.Instantiate(Resources.Load<GameObject>(AssetPath));

			if(!CharacterObject)
			{
				EditorTool.Log("[ Iplayer ] Can't find the asset path '" + AssetPath + "'.", LogType.Error);
				return;
			}

			Trans = CharacterObject.transform;
			Anim = CharacterObject.GetComponent<Animator>();
			Controller = CharacterObject.GetComponent<CharacterController>();
			Phview = CharacterObject.GetComponent<PhotonView>();

			Initialize();
		}

		/// <summary>
		/// 初始化角色資料.
		/// </summary>
		public virtual void Initialize()
		{


			EditorTool.Log("Character Initialized.", LogType.Normal);
		}

		/// <summary>
		/// 釋放角色記憶體資料. (用於角色死亡 or 遊戲關閉)
		/// </summary>
		public virtual void Release()
		{
			CharacterObject = null;
			Bones = null;
			Rigs = null;
			AnimStates.Clear();
		}
		
		/// <summary>
		/// 取得角色之遊戲物件.
		/// </summary>
		public GameObject GetGameObject ()
		{
			return CharacterObject;
		}

		/// <summary>
		/// 取得角色的Transform.
		/// </summary>
		public Transform GetTransform()
		{
			return Trans;
		}

		public CharacterController GetController()
		{
			return Controller;
		}

		public Animator GetAnimator()
		{
			return Anim;
		}

		/// <summary>
		/// 取得當前動畫狀態.
		/// </summary>
		public AnimatorStateInfo GetCurrentStateInfo(int layer)
		{
			return Anim.GetCurrentAnimatorStateInfo(layer);
		}

		/// <summary>
		/// 設定可否移動.
		/// </summary>
		public void SetMovable(bool active)
		{
			movable = active;
		}

		/// <summary>
		/// 是否可以移動.
		/// </summary>
		public bool IsMovable()
		{
			return movable;
		}

		/// <summary>
		/// 關閉特定Unity內建的Behaviour組件.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="behaviour"></param>
		/// <param name="active"></param>
		public void EnabledUnityBehaviour<T>(T behaviour, bool enabled) where T : Behaviour
		{
			T component = CharacterObject.GetComponent<T>();

			if(component)
			{
				component.enabled = enabled;
				return;
			}

			EditorTool.Log("[ SetUnityBehaviourEnabled ] Can't Find Component '" + component.name + "'.", LogType.Warning);
		}

		public void AddAnimatorState(int value)
		{
			//AnimStates.Add();
		}
	}
}


