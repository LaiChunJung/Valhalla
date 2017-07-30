using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class InputSystem : IGameSystem
	{
		// 建構子.
		public InputSystem() { }

		public void Initialize()
		{

		}
		
		public void Release()
		{

		}

		/// <summary>
		/// 射線點擊物件.
		/// </summary>
		public GameObject[] ClickObject(float maxDistance, int layerMask, int bufferLength)
		{
			GameObject[] resultObjs = new GameObject[bufferLength];

			RaycastHit[] hitInfos = new RaycastHit[bufferLength];

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			Physics.RaycastNonAlloc(ray, hitInfos, maxDistance, layerMask);

			for(int i = 0; i < hitInfos.Length; ++i)
			{
				resultObjs[i] = hitInfos[i].transform.gameObject;
			}

			return resultObjs;
		}
	}
}
