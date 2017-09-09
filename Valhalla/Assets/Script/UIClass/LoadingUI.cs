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

		public LoadingUI() : base() { }

		public override void Initialize()
		{
			loadingBar = UITool.GetChildUIComponent<Slider>(m_Root, "ProgressBar");
			progressText = UITool.GetChildUIComponent<Text>(m_Root, "ProgressText");
		}

		public override void Release()
		{
			loadingBar = null;
			progressText = null;
		}

		public void SetLoadingUIValue(float progress)
		{
			loadingBar.value = progress;
			progressText.text = string.Format("{0}{1}", (progress * 100.0f).ToString(), "%");
		}
	}
}
