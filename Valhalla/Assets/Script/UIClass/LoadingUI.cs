using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MovementEffects;

namespace Valhalla
{
	public class LoadingUI : IUserInterface<LoadingUI>
	{
		private Slider loadingBar;
		private Text progressText;

		public LoadingUI() : base()
		{
			Initialize();
		}

		public override void Initialize()
		{
			loadingBar = UITool.GetChildUIComponent<Slider>(GetRootObject(), "ProgressBar");
			progressText = UITool.GetChildUIComponent<Text>(GetRootObject(), "ProgressText");
		}

		private IEnumerator<float> _LoadLevelAsync(string levelName)
		{
			AsyncOperation async = SceneManager.LoadSceneAsync(levelName);

			while (!async.isDone)
			{
				EditorTool.Log(async.progress.ToString(), LogType.Normal);
				float progress = Mathf.Clamp01(async.progress / 0.9f);
				loadingBar.value = progress;
				progressText.text = string.Format("{0}{1}", (progress * 100.0f).ToString(), "%");
				yield return 0;
			}
		}

		public void LoadLevel(string levelName)
		{
			Timing.RunCoroutine(_LoadLevelAsync(levelName));
		}
	}
}
