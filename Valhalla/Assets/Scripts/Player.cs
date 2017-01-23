using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	private static Player _instance = null;
	public static Player Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<Player>();
				if (_instance == null)
				{
					Debug.LogWarning("Player is not exist.");
				}
			}
			return _instance;
		}
	}

	public float turnSpeed = 0.0f;
	public float jumpSpeed = 0.0f;
	public float jumpHeight = 0.0f;
	
	private Vector3 moveDir = Vector3.zero;
	private Transform cameraTrans;
	private static int Jump;
	private static int Dodge;

	protected override void Awake()
	{
		base.Awake();
		if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Jump = Animator.StringToHash("Base.Jump");
		Dodge = Animator.StringToHash("Base.Dodge");
	}

	protected override void Start()
	{
		base.Start();
		cameraTrans = CameraCtrl.Instance.transform;
		Debug.Log(Jump.ToString ());
		Debug.Log(Dodge.ToString());
	}

	void Update()
	{
		if (Input.GetButtonDown("Valhalla Dodge"))
		{
			Debug.Log(currentState.fullPathHash.ToString());
			if (!anim.IsInTransition (0) &&
				currentState.fullPathHash != Dodge &&
				currentState.fullPathHash != Jump)
			{
				anim.SetTrigger("Dodge");
			}
		}
	}

	public override void Move()
	{
		float h = 0.0f;
		float v = 0.0f;
		if (movable && currentState.tagHash != Dodge)
		{
			h = Input.GetAxis("Valhalla Horizontal");
			v = Input.GetAxis("Valhalla Vertical");
			anim.SetFloat("Run", Math.Abs(h) + Math.Abs(v));
		}

		moveDir = new Vector3(h, 0, v);

		if (currentState.fullPathHash == Jump)
		{
			moveDir = cameraTrans.TransformDirection(moveDir.normalized) * jumpSpeed;
			controller.Move(moveDir * Time.fixedDeltaTime);
		}
		else
		{
			moveDir = cameraTrans.TransformDirection(moveDir.normalized);
		}

		if(moveDir != Vector3.zero)
		{
			Vector3 targetDir = new Vector3(moveDir.x, 0, moveDir.z);
			trans.forward = Vector3.Lerp(trans.forward, targetDir, turnSpeed * Time.fixedDeltaTime);
		}
	}

	void AnimStart(string msg)
	{
		//Debug.Log(msg);
		switch (msg)
		{
			case "Dodge":
				movable = false;
				break;
			default:
				break;
		}
	}

	void AnimOver(string msg)
	{
		//Debug.Log(msg);
		switch (msg)
		{
			case "Dodge":
				movable = true;
				break;
			default:
				break;
		}
	}
}
