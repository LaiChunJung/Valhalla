﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class FakerServer : MonoBehaviour
	{
		private static FakerServer _instance = null;
		public static FakerServer Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<FakerServer>();

					if (_instance == null)
					{
						GameObject manager = new GameObject("FakerServer");
						_instance = manager.AddComponent<FakerServer>();
					}
				}
				return _instance;
			}
		}

		private List<IPlayer> SPlayerList = new List<IPlayer>();

		public string GetPlayerlist(int Playerindex)
		{
			return SPlayerList[Playerindex].GetGameObject().name;
		}

		public void AddPlayerlist(IPlayer PlayerScript)
		{
			if (SPlayerList == null)
			{
				Debug.Log("~~~〒▽〒~~~");
				return;
			}
			SPlayerList.Add(PlayerScript);
			Debug.Log("~~~~~~<(￣︶￣)/ / / " + SPlayerList.Count);
		}

		public void RemovePlayerList()
		{

		}
	}
}

