using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class Magi : IPlayer
	{
		public Magi(Vector3 position, Quaternion rotation)
		{
			assetFullPath = string.Format("{0}/{1}", AssetPath.Player, "Magi");
			Init(position, rotation);
		}
	}
}