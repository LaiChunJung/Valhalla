using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
	public float				hp = 0.0f;
	public bool					movable = true;
	public bool					hitable = true;
	protected Transform			trans;
	protected Animator			anim;
	protected PhotonView        phview;
	public AnimatorStateInfo	currentState
	{
		get
		{
			return anim.GetCurrentAnimatorStateInfo(0);
		}
	}
	public float				rotSpeed = 0.0f;
	public float				jumpSpeed = 0.0f;
	public float				jumpHeight = 0.0f;
	
	protected CharacterController	controller;
	protected const float			gravity = 10.0f;
	protected Vector3				moveDir = Vector3.zero;
	protected float					horizontal = 0.0f;
	protected float					vertical = 0.0f;
	protected Collider[]			bones;
	protected Rigidbody[]			rigs;

	protected virtual void Awake()
	{
		trans = this.transform;
		anim = GetComponent<Animator> ();
        phview = GetComponent<PhotonView>();
	}

	protected virtual void Start()
	{
		controller = GetComponent<CharacterController>();
	}
}
