using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class Magi : IPlayer
	{
		private const string assetFileName = "Magi";

		public Magi(Vector3 position, Quaternion rotation)
		{
			GameObject asset = Resources.Load<GameObject>(string.Format("{0}/{1}", AssetPath.Player, assetFileName));

			if(!asset)
			{
				Debug.LogWarning();
				return;
			}

			characterObject = Object.Instantiate(asset, position, rotation);

			characterObject.name = assetFileName;

			Initialize();
		}
	}
}