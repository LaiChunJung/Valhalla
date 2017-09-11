using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Valhalla
{
	public class CharacterSystem : ISystem
	{
		public Vector3 StartPosition
		{
			get
			{
				GameObject temp;

				temp = GameObject.Find("Start Position");

				if(!temp)
				{
					Debug.LogWarning("[ StartPosition ] Can't find the gameobject 'StartPosition'.");

					return Vector3.zero;
				}

				return GameObject.Find("Start Position").transform.position;
			}

			private set { }
		}

		private Transform player;

		public CharacterSystem() { }

		public void Initialize()
		{
			CreateCharacter("Magi", StartPosition, Quaternion.identity);

			PlayerCamera.Instance.active = true;
			PlayerCamera.Instance.SetTarget(player);
		}

		public void Release()
		{
			player = null;
		}

		/// <summary>
		/// 建立玩家角色. (未完成)
		/// </summary>
		/// <param name="fileName">資源檔案名稱.</param>
		/// <param name="position">初始位置.</param>
		/// <param name="rotation">初始角度.</param>
		/// <returns></returns>
		public GameObject CreateCharacter(string fileName, Vector3 position, Quaternion rotation)
		{
			string fullPath = string.Format("{0}{1}{2}", AssetPath.Player, "/", fileName);
			Transform playerTemp = Resources.Load<Transform>(fullPath);

			// Test.
			if(playerTemp == null)
			{
				Debug.LogWarning("[ CreateCharacter ] Can not find the asset:" + fullPath + ".");
				return null;
			}

			player = UnityEngine.Object.Instantiate(playerTemp, position, rotation);

			player.name = fileName;

			return player.gameObject;
		}
	}
}
