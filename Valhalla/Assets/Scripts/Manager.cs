using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	private static Manager _instance = null;
	public static Manager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<Manager>();

				if (_instance == null)
				{
					GameObject manager = new GameObject("Manager");
					_instance = manager.AddComponent<Manager>();
				}
			}
			return _instance;
		}
	}

	void Awake()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
	}

	void Start ()
	{
		
	}

	void Update()
	{
		Player.Instance.InputDetect();
	}
	
	void FixedUpdate()
	{
		
	}

	void LateUpdate()
	{
		Player.Instance.Move();
		Player.Instance.IKControl();
		CameraCtrl.Instance.Caculate();
	}
}
