using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	/// <summary>
	/// 角色控制器.
	/// </summary>
	public class PlayerController
	{
		private const string horizontal = "Valhalla Horizontal";
		private const string vertical = "Valhalla Vertical";

		public void Initialize()
		{

		}

		public void SystemUpdate()
		{
			if (Input.GetAxis(horizontal) != 0 || Input.GetAxis(vertical) != 0)
			{

			}
		}

		public void Release()
		{

		}
	}
}