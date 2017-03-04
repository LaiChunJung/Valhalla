using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	#region ------ Public Varibles ------
	public Rigidbody			deadCameraConneted;
	public float				ikOffsetY;
	public float				fallingMovSpd = 0.0f;
	#endregion

	#region ------ Private Varibles ------
	private CharacterController LogController;
	private Vector3				lFootPos;
	private Vector3				rFootPos;
	private Quaternion			lFootRot;
	private Quaternion			rFootRot;
	private float				lFootWt;
	private float				rFootWt;
	private Transform			leftFoot;
	private Transform			rightFoot;
	private Transform			cameraTrans;
	#endregion

	#region ------ Animation States ------
	private static int Jump;
	private static int Dodge;
	private static int Fall;
	#endregion


	protected override void Awake()
	{
		base.Awake();

		cameraTrans = CameraCtrl.Instance.transform;

		Manager.Instance.UpdateDel += InputDetect;
		Manager.Instance.UpdateDel += Movement;
		Manager.Instance.LateUpdateDel += IKControl;
	}


	protected override void Start()
	{
		base.Start();

		

		leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
		rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

		bones = GetComponentsInChildren<Collider>(true);
		foreach (Collider bone in bones)
		{
			Physics.IgnoreCollision(controller, bone, true);
		}

		Jump = Animator.StringToHash("Base.Jump");
		Dodge = Animator.StringToHash("Base.Dodge");
		Fall = Animator.StringToHash("Base.Fall");
	}


	void OnAnimatorIK()
	{
		lFootWt = anim.GetFloat("LeftFoot");
		rFootWt = anim.GetFloat("RightFoot");

		if(controller.isGrounded)
		{
			anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFootWt);
			anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFootWt);
			anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFootRot);
			anim.SetIKRotation(AvatarIKGoal.RightFoot, rFootRot);
		}

		anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFootPos);
		anim.SetIKPosition(AvatarIKGoal.RightFoot, rFootPos);

		anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFootWt);
		anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFootWt);
	}


	// ------ LateUpdate ------
	public void IKControl ()
	{
		if (!anim || !controller)
			return;

		RaycastHit lFootHit;
		RaycastHit rFootHit;

		Vector3 lPos = leftFoot.position + new Vector3 (0, 0.1f, 0);
		Vector3 rPos = rightFoot.position + new Vector3(0, 0.1f, 0);

		if (Physics.Raycast(lPos, -Vector3.up, out lFootHit, 0.9f, ~LayerMask.GetMask("Player")))
		{
			lFootPos = lFootHit.point;
			lFootRot = Quaternion.FromToRotation(trans.up, lFootHit.normal) * trans.rotation;
			lFootRot = Quaternion.Euler(lFootRot.eulerAngles.x, lFootRot.eulerAngles.y, 0);
			Vector3 curLPos = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
			float lDis = Vector3.Distance(curLPos, lFootPos);
			if (lDis > 0.1f)
			{
				Vector3 correct = lFootPos - curLPos;
				lFootPos = curLPos + correct * 0.2f;
			}
			else
				lFootPos = lFootPos + new Vector3(0, ikOffsetY, 0);

			//Debug.Log("Left Foot Pos : " + lFootPos.ToString () + "   " + "Left Foot Rot : " + lFootRot.eulerAngles.ToString ());
		}

		if (Physics.Raycast(rPos, -Vector3.up, out rFootHit, 0.9f, ~LayerMask.GetMask("Player")))
		{
			rFootPos = rFootHit.point;
			rFootRot = Quaternion.FromToRotation(trans.up, rFootHit.normal) * trans.rotation;
			rFootRot = Quaternion.Euler(rFootRot.eulerAngles.x, rFootRot.eulerAngles.y, 0);
			Vector3 curRPos = anim.GetBoneTransform(HumanBodyBones.RightFoot).position;
			float rDis = Vector3.Distance(curRPos, rFootPos);
			if (rDis > 0.1f)
			{
				Vector3 correct = rFootPos - curRPos;
				rFootPos = curRPos + correct * 0.2f;
			}
			else
				rFootPos = rFootPos + new Vector3 (0, ikOffsetY, 0);

			//Debug.Log("Right Foot Pos : " + rFootPos.ToString () + "   " + "Right Foot Rot : " + rFootRot.eulerAngles.ToString());
		}
	}


	// ------ Update ------
	public void InputDetect()
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


	// ------ Update ------
	protected override void Movement()
	{
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
				Debug.Log(hit.distance.ToString());
				if (hit.distance > 1.0f)
					anim.SetBool("Fall", true);
				else
					anim.SetBool("Fall", false);
			}
		}
		// --- Falling End ---
	}

	public void SetPlayerDead()
	{
		anim.enabled = false;
		movable = false;
		hitable = false;
		gameObject.AddComponent<FixedJoint>().connectedBody = deadCameraConneted;
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
}
