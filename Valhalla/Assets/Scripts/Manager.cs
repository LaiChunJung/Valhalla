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

    public bool PlayerJoin = false;

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
		//Player.Instance.SetFalling();
	}

	void Update()
	{
        if(PlayerJoin)
        {
            Player.Instance.InputDetect();
            FpsDisplay.Instance.Launch();

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Player.Instance.SetPlayerOutOfCtrl();
            }
        }
	}
	
	void FixedUpdate()
	{
		
	}

	void LateUpdate()
	{
        if (PlayerJoin)
        {
            PlayerMovement.Instance.Move();
            Player.Instance.SetFalling();
            Player.Instance.IKControl();
            CameraCtrl.Instance.Caculate();
        }
	}
}
