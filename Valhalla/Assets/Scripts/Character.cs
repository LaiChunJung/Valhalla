using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	// --- Public ---
	public float				hp = 0.0f;
	public bool					movable = true;
	public bool					hitable = true;
	[HideInInspector]
	public Transform			trans;
	[HideInInspector]
	public Animator				anim;
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
	// --- Public End ---

	// --- Protected ---
	protected CharacterController	controller;
	protected const float			gravity = 10.0f;
	protected Vector3				moveDir = Vector3.zero;
	protected float					horizontal = 0.0f;
	protected float					vertical = 0.0f;
	protected Collider[]			bones;
	// --- Protected End ---

	protected virtual void Awake()
	{
		trans = this.transform;
		anim = GetComponent<Animator> ();
	}

	protected virtual void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	protected virtual void Movement()
	{
		//How to move...
	}

    [PunRPC]
    public void PunSetAni(string AniName, float Distance)
    {
        anim.SetFloat("Run", Distance);
    }
}
