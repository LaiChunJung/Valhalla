using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valhalla
{
	public class IPlayer : ICharacter
	{
		private const int Jump = 0;
		private const int Dodge = 0;
		private const int Fall = 0;

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

