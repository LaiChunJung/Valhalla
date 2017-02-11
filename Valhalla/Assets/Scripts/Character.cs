using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float hp = 0.0f;
	public bool movable = true;
	public bool hitable = true;
	[HideInInspector]
	public Transform trans;
	[HideInInspector]
	public Animator anim;
	public AnimatorStateInfo currentState
	{
		get
		{
			return anim.GetCurrentAnimatorStateInfo(0);
		}
	}

	protected virtual void Awake()
	{
		trans = this.transform;
		anim = GetComponent<Animator> ();
	}

	protected virtual void Start()
	{
		
	}

	public virtual void Move()
	{
		//How to move...
	}
}
