using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour
{
	private static CameraCtrl _instance = null;
	public static CameraCtrl Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(CameraCtrl)) as CameraCtrl;

				if (_instance == null)
				{
					GameObject cam = new GameObject("CameraCtrl");
					cam.AddComponent<Camera>();
					_instance = cam.AddComponent<CameraCtrl>();
				}
			}
			return _instance;
		}
	}

	public bool HideAndShowCursor = true;
	public bool LockRotationWhenRightClick = false;
	public bool UseBlurEffect = true;
	public bool UseFogEffect = true;
	public Transform target;

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

	[Range (0.0f, 1.0f)]
	public float sensitivity = 0.5f;

	private float x = 0.0f;
	private float y = 0.0f;
	private float currentDistance;
	private float desiredDistance;
	private float correctedDistance;
	private Transform trans;


	void Awake()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		trans = transform;

		if (!target)
			Debug.LogWarning("Target is not accessed.");
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Vector3 angles = trans.eulerAngles;
		x = angles.y;
		y = angles.x;

		currentDistance = distance;
		desiredDistance = distance;
		correctedDistance = distance - 0.2f;
	}

	public void Caculate()
	{
		// Don't do anything if target is not defined
		if (!target)
			return;

		// If either mouse buttons are down, let the mouse govern camera position
		if (LockRotationWhenRightClick == false)
		{
			x += Input.GetAxis("Valhalla Mouse X") * xSpeed * Time.fixedDeltaTime;
			y -= Input.GetAxis("Valhalla Mouse Y") * ySpeed * Time.fixedDeltaTime;
		}
		if (Input.GetMouseButton(0))
		{
			if (LockRotationWhenRightClick == false)
			{
				x += Input.GetAxis("Valhalla Mouse X") * xSpeed * Time.fixedDeltaTime;
				y -= Input.GetAxis("Valhalla Mouse Y") * ySpeed * Time.fixedDeltaTime;
			}
		}
		y = ClampAngle(y, yMinLimit, yMaxLimit);

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
		if (Physics.Linecast(trueTargetPosition, position, out collisionHit, ~LayerMask.GetMask ("Player")))
		{
			if (collisionHit.transform.name != target.name)
			{
				position = collisionHit.point + collisionHit.normal.normalized/2;
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
}