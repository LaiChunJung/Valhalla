using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Valhalla
{
	public class LogoUI : IUserInterface<LogoUI>
	{
		private Text teamLogoTxet;
		private Text gameLogoText;

		public readonly float fadeDuration = 1.5f;

		public LogoUI(): base()
		{
			teamLogoTxet = GetChildUIComponent<Text>("Text_TeamLogo");
			gameLogoText = GetChildUIComponent<Text>("Text_GameLogo");

			EditorTool.Log("LogoUI is initialized.", LogType.Normal);
		}

		public override void Initialize()
		{
			teamLogoTxet.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			teamLogoTxet.DOFade(1.0f, fadeDuration).OnComplete( () => 
			{
				teamLogoTxet.DOFade(0.0f, fadeDuration).OnComplete(() =>
				{
					gameLogoText.DOFade(1.0f, fadeDuration).OnComplete(() =>
					{
						gameLogoText.DOFade(0.0f, fadeDuration).OnComplete(()=> 
						{

						});
					});
				});
			});
		}

		public override void Release() { }
	}
}
