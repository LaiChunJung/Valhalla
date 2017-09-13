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

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Release()
		{
			base.Release();
		}
	}

}

