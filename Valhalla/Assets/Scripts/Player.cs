using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	private struct PlayerMotion
	{
		public int Jump;
		public int Dodge;
	}

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
					Debug.LogError("Player is not exist.");
				}
			}
			return _instance;
		}
	}

	public float turnSpd = 0.0f;

	private PlayerMotion Motion;
	private Vector3 moveDir = Vector3.zero;

	protected override void Awake()
	{
		base.Awake();
		if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
	}

	protected override void Start()
	{
		base.Start();
		Motion.Jump = Animator.StringToHash("Base.Jump");
		Motion.Dodge = Animator.StringToHash("Base.Dodge");
	}

	public override void Move()
	{
		float h = 0.0f;
		float v = 0.0f;
		if (movable && currentState.tagHash != Motion.Dodge)
		{
			h = Input.GetAxis("Valhalla Horizontal");
			v = Input.GetAxis("Valhalla Vertical");
			anim.SetFloat("Run", Math.Abs(h) + Math.Abs(v));
		}

		moveDir = new Vector3(h, 0, v);

		if (currentState.tagHash == Motion.Jump)
		{
			//moveDir = direction.TransformDirection(moveDir.normalized) * moveSpeed;
			controller.Move(moveDir * Time.fixedDeltaTime);
		}

		if(moveDir != Vector3.zero)
		{
			Vector3 targetDir = new Vector3(moveDir.x, 0, moveDir.z);
			trans.forward = Vector3.Lerp(trans.forward, targetDir, turnSpd * Time.fixedDeltaTime);
		}
	}
}
