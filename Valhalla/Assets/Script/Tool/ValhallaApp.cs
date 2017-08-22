using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; 
using MovementEffects;

namespace Valhalla
{
	public static class ValhallaApp
	{
		private static Image loadingPorgressBar;

		public static void LoadScene(string levelName)
		{

		}

		public static void RemoveComponent<Component>(this GameObject obj)
		{
			Component component = obj.GetComponent<Component>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component as UnityEngine.Object);
			}
		}
	}
}
