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

	//public List<GameObject> players;
	public delegate void Action();
	public Action UpdateDel;
	public Action FixedUpdateDel;
	public Action LateUpdateDel;

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
		if (UpdateDel != null)
			UpdateDel();

		if (PlayerJoin)
        {
            //Player.Instance.InputDetect();
            //FpsDisplay.Instance.Launch();

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                //Player.Instance.SetPlayerOutOfCtrl();
            }
        }
	}
	
	void FixedUpdate()
	{
		if (FixedUpdateDel != null)
			FixedUpdateDel();
	}

	void LateUpdate()
	{
		if(LateUpdateDel != null)
			LateUpdateDel();

		if (PlayerJoin)
		{

		}
	}
}
