using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	public float				fallingMovSpd = 0.0f;
	
	private CharacterController LogController;
	private Transform			cameraTrans;

	private static int			Jump;
	private static int			Dodge;
	private static int			Fall;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();

		Jump = Animator.StringToHash("Base.Jump");
		Dodge = Animator.StringToHash("Base.Dodge");
		Fall = Animator.StringToHash("Base.Fall");

		bones = GetComponentsInChildren<Collider>(true);
		rigs = GetComponentsInChildren<Rigidbody>();

		foreach (Collider bone in bones)
		{
			Physics.IgnoreCollision(controller, bone, true);
		}
	}
	
	// ------ Update ------
	public void InputCtrl()
	{
		if (!anim || !controller)
			return;

		if (movable)
		{
			if (currentState.fullPathHash != Dodge)
			{
				horizontal = Input.GetAxis("Valhalla Horizontal");
				vertical = Input.GetAxis("Valhalla Vertical");

                anim.SetFloat("Run", Math.Abs(horizontal) + Math.Abs(vertical));
                //phview.RPC("PunSetAni", PhotonTargets.All, "Run", anim.GetFloat("Run"));
            }

			moveDir = new Vector3(horizontal, 0, vertical);
			moveDir = cameraTrans.TransformDirection(moveDir);
		}

		if (Input.GetButtonDown("Valhalla Dodge") &&
			controller.isGrounded)
		{
			if (!anim.IsInTransition(0) &&
				currentState.fullPathHash != Jump)
			{
				anim.SetTrigger("Dodge");
			}
		}
		else if (Input.GetButtonDown("Valhalla Jump"))
		{
			if (!anim.IsInTransition(0) &&
				currentState.fullPathHash != Dodge && 
				controller.isGrounded)
			{
				anim.SetTrigger("Jump");
				moveDir.y = 1000.0f;
			}
		}
	}
	
	public void Active()
	{
		if (!enabled)
			return;

		if (currentState.fullPathHash == Fall)
		{
			controller.Move(moveDir.normalized * fallingMovSpd * Time.fixedDeltaTime);
		}

		if (moveDir != Vector3.zero)
		{
			trans.forward =
				Vector3.Lerp(trans.forward, new Vector3(moveDir.x, 0, moveDir.z), rotSpeed * Time.fixedDeltaTime);
		}

		// --- Falling Begin ---
		if (currentState.fullPathHash != Jump &&
			currentState.fullPathHash != Dodge &&
			!controller.isGrounded)
		{
			RaycastHit hit;
			if (Physics.SphereCast(trans.position + new Vector3(0.0f, 0.3f, 0.0f),
				controller.radius,
				Vector3.down, out hit, float.MaxValue,
				~LayerMask.GetMask("Player")))
			{
				//Debug.Log(hit.distance.ToString());
				if (hit.distance > 1.0f)
					anim.SetBool("Fall", true);
				else
					anim.SetBool("Fall", false);
			}
		}
		// --- Falling End ---
	}

	void AnimStart(string msg)
	{
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
		switch (msg)
		{
			case "Dodge":
				movable = true;
				break;
			default:
				break;
		}
	}

    // ------ OnTrigger -----
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("(￣ε(#￣)☆╰╮o(￣▽￣///) ");
            if (phview.isMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

	public void OnButtonTestPlayerDead()
	{
		rigs[0].constraints = RigidbodyConstraints.None;
		anim.enabled = false;
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}
}
