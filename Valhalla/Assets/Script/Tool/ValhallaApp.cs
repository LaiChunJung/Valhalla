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
	public class ValhallaApp
	{
		private static AsyncOperation loadSceneAsync;

		public static void LoadScene(string levelName)
		{
			Timing.RunCoroutine(_LoadSceneCoroutine(levelName));
		}

		private static IEnumerator<float> _LoadSceneCoroutine(string sceneName)
		{
			loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
			loadSceneAsync.allowSceneActivation = false;

			while (!loadSceneAsync.isDone)
			{
				//Core.GetSystem<LoadingUI>().Show();
				Core.GetSystem<LoadingUI>().GetRootObject().SetActive(true);
				Core.GetSystem<LoadingUI>().SetLoadingUIValue(loadSceneAsync.progress / 0.9f);

				if(loadSceneAsync.progress >= 0.9f)
				{
					yield return Timing.WaitForSeconds(0.5f);
					loadSceneAsync.allowSceneActivation = true;
				}

				yield return 0;
			}
		}
	}
}
