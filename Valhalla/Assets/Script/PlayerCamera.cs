using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class PlayerCamera : IMonoSingleton<PlayerCamera>
	{
		public bool active = false;
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

		private Transform target;
		private float x = 0.0f;
		private float y = 0.0f;
		private float currentDistance;
		private float desiredDistance;
		private float correctedDistance;
		private Transform trans;

		private void Awake()
		{
			trans = transform;
			
			if (!target)
				Debug.LogWarning("Target is not accessed.");

			Vector3 angles = trans.eulerAngles;
			x = angles.y;
			y = angles.x;

			currentDistance = distance;
			desiredDistance = distance;
			correctedDistance = distance - 0.2f;

			Cursor.visible = false;
		}

		private void LateUpdate()
		{
			Active();
		}

		public void OnHideCursor()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		
		public void Active()
		{
			if(!active)
				return;

			if(!target)
				return;

			x += Input.GetAxis("Valhalla Mouse X") * xSpeed * Time.fixedDeltaTime;
			y -= Input.GetAxis("Valhalla Mouse Y") * ySpeed * Time.fixedDeltaTime;

			x = ClampAngle(x, -360.0f, 360f);
			y = ClampAngle(y, yMinLimit, yMaxLimit);

			//x = Mathf.Clamp(x, -360.0f, 360.0f);
			//y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

			// set camera rotation
			Quaternion rotation = Quaternion.Euler(y, x, 0);

			// calculate the desired distance
			desiredDistance -=
				Input.GetAxis("Valhalla Mouse ScrollWheel") * Time.fixedDeltaTime * zoomRate * Mathf.Abs(desiredDistance);
			desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
			correctedDistance = Mathf.Lerp(correctedDistance, desiredDistance, sensitivity);

			// calculate desired camera position
			Vector3 position =
				target.position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));

			// check for collision using the true target's desired registration point as set by user using height
			RaycastHit collisionHit;
			Vector3 trueTargetPosition =
				new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

			// if there was a collision, correct the camera position and calculate the corrected distance
			bool isCorrected = false;
			if (Physics.Linecast(trueTargetPosition, position, out collisionHit, ~LayerMask.GetMask("Player")))
			{
				if (collisionHit.transform.name != target.name)
				{
					position = collisionHit.point + collisionHit.normal.normalized / 2;
					correctedDistance = Vector3.Distance(trueTargetPosition, position);
					isCorrected = true;
				}
			}

			// For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
			currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.fixedDeltaTime * zoomDampening) : correctedDistance;

			// recalculate position based on the new currentDistance
			position = target.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight - 0.05f, 0));

			trans.rotation = Quaternion.Lerp(trans.rotation, rotation, sensitivity);
			trans.position = Vector3.Lerp(trans.position, position, sensitivity);
		}

		private static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360)
				angle += 360;
			if (angle > 360)
				angle -= 360;
			return Mathf.Clamp(angle, min, max);
		}

		public void SetTarget(Transform _target)
		{
			target = _target;
		}
	}
}