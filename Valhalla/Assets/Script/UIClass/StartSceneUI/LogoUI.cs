using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;					// 使用DoTween功能的命名空間.
using MovementEffects;				// 使用特化Coroutine功能的命名空間.

namespace Valhalla
{
	public class LogoUI : IUserInterface<LogoUI>
	{
		private Text teamLogoTxet;
		private Text gameLogoText;

		private readonly float fadeTime = 1.5f;
		private readonly float logoInterval = 0.5f;

		public LogoUI() : base()
		{
			Initialize();
		}

		public override void Initialize()
		{
			teamLogoTxet = GetChildUIComponent<Text>("Text_TeamLogo");
			gameLogoText = GetChildUIComponent<Text>("Text_GameLogo");
		}

		public override void Release() { }

		/// <summary>
		/// 開始播放Logo.
		/// </summary>
		public void StartLogo(TweenCallback cb_OnComplete)
		{
			teamLogoTxet.DOFade(1.0f, fadeTime).SetDelay(logoInterval).OnComplete(() =>
			{
				teamLogoTxet.DOFade(0.0f, fadeTime).OnComplete(()=> 
				{
					gameLogoText.DOFade(1.0f, fadeTime).SetDelay(logoInterval).OnComplete(() =>
					{
						gameLogoText.DOFade(0.0f, fadeTime).OnComplete(cb_OnComplete);
					});
				});
			});
		}
	}
}
