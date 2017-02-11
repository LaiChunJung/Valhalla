using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
	private static PlayerMovement _instance = null;
	public static PlayerMovement Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<PlayerMovement>();

				if (_instance == null)
				{
					_instance = Player.Instance.gameObject.AddComponent<PlayerMovement>();
				}
			}
			return _instance;
		}
	}

	private Vector3 moveDir = Vector3.zero;
	private Transform cameraTrans;
	public float turnSpeed = 0.0f;
	public float jumpSpeed = 0.0f;
	public float jumpHeight = 0.0f;
	public float fallMoveSpeed = 0.0f;

	#region ------ Animation States ------
	private static int Jump;
	private static int Dodge;
	private static int Fall;
	#endregion

	void Awake()
	{
		if (Instance != this)
		{
			Destroy(this);
			return;
		}
	}

	void Start()
	{
		cameraTrans = CameraCtrl.Instance.transform;

		Jump = Animator.StringToHash("Base.Jump");
		Dodge = Animator.StringToHash("Base.Dodge");
		Fall = Animator.StringToHash("Base.Fall");
	}

	public void Move()
	{
		if (!Player.Instance.anim || !Player.Instance.controller)
			return;

		float h = 0.0f;
		float v = 0.0f;

		if (Player.Instance.movable && Player.Instance.currentState.tagHash != Dodge)
		{
			h = Input.GetAxis("Valhalla Horizontal");
			v = Input.GetAxis("Valhalla Vertical");
			Player.Instance.anim.SetFloat("Run", Math.Abs(h) + Math.Abs(v));
		}

		moveDir = new Vector3(h, 0, v);

		if (Player.Instance.currentState.fullPathHash == Jump)
		{
			moveDir = cameraTrans.TransformDirection(moveDir.normalized) * jumpSpeed;
			Player.Instance.controller.Move(moveDir * Time.fixedDeltaTime);
		}
		else if (Player.Instance.currentState.fullPathHash == Fall && !Player.Instance.anim.IsInTransition(0))
		{
			moveDir = cameraTrans.TransformDirection(moveDir.normalized) * fallMoveSpeed;
			Player.Instance.controller.Move(moveDir * Time.fixedDeltaTime);
		}
		else
		{
			moveDir = cameraTrans.TransformDirection(moveDir.normalized);
		}

		if (moveDir != Vector3.zero)
		{
			Vector3 targetDir = new Vector3(moveDir.x, 0, moveDir.z);
			Player.Instance.trans.forward = 
				Vector3.Lerp(Player.Instance.trans.forward, targetDir, turnSpeed * Time.fixedDeltaTime);
		}
	}
}
