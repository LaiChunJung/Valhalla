using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	#region ------ Singleton ------
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
	#endregion

	#region ------ Public Varibles ------
	public float		turnSpeed = 0.0f;
	public float		jumpSpeed = 0.0f;
	public float		jumpHeight = 0.0f;
	public Collider[]	bones;
	
	public float		ikOffsetY;
	#endregion

	#region ------ Private Varibles ------
	private Vector3		moveDir = Vector3.zero;
	private Transform	cameraTrans;
	private Vector3		lFootPos;
	private Vector3		rFootPos;
	private Quaternion	lFootRot;
	private Quaternion	rFootRot;
	private float		lFootWt;
	private float		rFootWt;
	private Transform	leftFoot;
	private Transform	rightFoot;
	#endregion

	#region ------ Animation States ------
	private static int Jump;
	private static int Dodge;
	#endregion

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

		Jump = Animator.StringToHash("Base.Jump");
		Dodge = Animator.StringToHash("Base.Dodge");

		leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
		rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

		cameraTrans = CameraCtrl.Instance.transform;

		foreach (Collider bone in bones)
		{
			Physics.IgnoreCollision(controller, bone, true);
		}
	}

	void OnAnimatorIK()
	{
		lFootWt = anim.GetFloat("LeftFoot") + anim.GetFloat("FootIK");
		rFootWt = anim.GetFloat("RightFoot") + anim.GetFloat("FootIK");

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

	//------LateUpdate------
	public void IKControl ()
	{
		RaycastHit lFootHit;
		RaycastHit rFootHit;

		Vector3 lPos = leftFoot.TransformPoint(Vector3.zero) + new Vector3 (0, 0.2f, 0);
		Vector3 rPos = rightFoot.TransformPoint(Vector3.zero) + new Vector3(0, 0.2f, 0);

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
				lFootPos = curLPos + correct * 0.65f;
			}
			else
				lFootPos = lFootPos + new Vector3(0, ikOffsetY, 0);

			Debug.Log("Left Foot Pos : " + lFootPos.ToString () + "   " + "Left Foot Rot : " + lFootRot.eulerAngles.ToString ());
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
				rFootPos = curRPos + correct * 0.65f;
			}
			else
				rFootPos = rFootPos + new Vector3 (0, ikOffsetY, 0);

			Debug.Log("Right Foot Pos : " + rFootPos.ToString () + "   " + "Right Foot Rot : " + rFootRot.eulerAngles.ToString());
		}
	}

	//------Update------
	public void InputDetect()
	{
		if (Input.GetButtonDown("Valhalla Dodge"))
		{
			if (!anim.IsInTransition(0) &&
				currentState.fullPathHash != Dodge &&
				currentState.fullPathHash != Jump)
			{
				anim.SetTrigger("Dodge");
			}
		}
		else if (Input.GetButtonDown("Valhalla Jump") && controller.isGrounded)
		{
			if (!anim.IsInTransition(0) &&
				currentState.fullPathHash != Dodge &&
				currentState.fullPathHash != Jump)
			{
				anim.SetTrigger("Jump");
			}
		}
	}

	//------LateUpdate------
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
