﻿using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class PlayerCamera : IMonoSingleton<PlayerCamera>
	{
		public float targetHeight = 1.0f;
		public float maxDistance = 10.0f;
		public float minDistance = 1.0f;
		public float distance = 10.0f;
		public float xSpeed = 250.0f;
		public float ySpeed = 120.0f;
		public int yMinLimit = -80;
		public int yMaxLimit = 80;
		public int zoomRate = 40;
		public float rotationDampening = 3.0f;
		public float zoomDampening = 10.0f;
		[Range(0.0f, 1.0f)]
		public float sensitivity = 0.5f;

		private ICharacter target;
		private Transform trans;
		private bool active = true;
		private float x = 0.0f;
		private float y = 0.0f;
		private float currentDistance;
		private float desiredDistance;
		private float correctedDistance;
		private float MouseX
		{
			get
			{
				return Core.GetSystem<PlayerController>().Input_MouseX;
			}

			set { }
		}
		private float MouseY
		{
			get
			{
				return Core.GetSystem<PlayerController>().Input_MouseY;
			}
			set { }
		}
		private float MouseScrollWheel
		{
			get
			{
				return Core.GetSystem<PlayerController>().Input_MouseScrollWheel;
			}
			set { }
		}

		private void Awake()
		{
			trans = transform;

			Vector3 angles = trans.eulerAngles;
			x = angles.y;
			y = angles.x;

			currentDistance = distance;
			desiredDistance = distance;
			correctedDistance = distance - 0.2f;
		}

		private void LateUpdate()
		{
			Active();
		}
		
		public void Active()
		{
			if(!active)
				return;

			if(target == null)
				return;

			x += MouseX * xSpeed * Time.fixedDeltaTime;
			y -= MouseY * ySpeed * Time.fixedDeltaTime;

			x = Math.ClampAngle(x, -360.0f, 360f);
			y = Math.ClampAngle(y, yMinLimit, yMaxLimit);

			// set camera rotation
			Quaternion rotation = Quaternion.Euler(y, x, 0);

			// calculate the desired distance
			desiredDistance -=
				MouseScrollWheel * Time.fixedDeltaTime * zoomRate * Mathf.Abs(desiredDistance);
			desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
			correctedDistance = Mathf.Lerp(correctedDistance, desiredDistance, sensitivity);

			// calculate desired camera position
			Vector3 position =
				target.GetTransform().position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));

			// check for collision using the true target's desired registration point as set by user using height
			RaycastHit collisionHit;
			Vector3 trueTargetPosition =
				new Vector3(
					target.GetTransform().position.x, 
					target.GetTransform().position.y + targetHeight, 
					target.GetTransform().position.z);

			// if there was a collision, correct the camera position and calculate the corrected distance
			bool isCorrected = false;
			if (Physics.Linecast(trueTargetPosition, position, out collisionHit, ~LayerMask.GetMask("Player")))
			{
				if (collisionHit.transform.name != target.GetGameObject().name)
				{
					position = collisionHit.point + collisionHit.normal.normalized / 2;
					correctedDistance = Vector3.Distance(trueTargetPosition, position);
					isCorrected = true;
				}
			}

			// For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
			currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.fixedDeltaTime * zoomDampening) : correctedDistance;

			// recalculate position based on the new currentDistance
			position = target.GetTransform().position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight - 0.05f, 0));

			trans.rotation = Quaternion.Lerp(trans.rotation, rotation, sensitivity);
			trans.position = Vector3.Lerp(trans.position, position, sensitivity);
		}

		/// <summary>
		/// 設定攝影機目標.
		/// </summary>
		/// <param name="_target"></param>
		public void SetTarget(ICharacter character)
		{
			target = character;
		}

		/// <summary>
		/// 取得Transform.
		/// </summary>
		/// <returns></returns>
		public Transform GetTransform()
		{
			return trans;
		}

		/// <summary>
		/// 取得攝影機水平方向.
		/// </summary>
		/// <returns></returns>
		public Vector3 GetHorizontalForward()
		{
			return new Vector3(trans.forward.x, 0.0f, trans.forward.z);
		}
	}
}