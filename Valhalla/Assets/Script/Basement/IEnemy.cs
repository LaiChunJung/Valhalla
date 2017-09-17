using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class IEnemy : ICharacter
	{
		public IEnemy ()
		{

		}

		protected override void Init(Vector3 position, Quaternion rotation)
		{
			base.Init(position, rotation);
		}

		public override void Release()
		{
			base.Release();
		}
	}
}


