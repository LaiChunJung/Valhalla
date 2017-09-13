using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class Magi : IPlayer
	{
		public Magi(Vector3 position, Quaternion rotation)
		{
			Create("Magi", position, rotation);

			Initialize();
		}
	}
}