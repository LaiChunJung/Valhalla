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

		public LogoUI(): base() { }

		public override void Initialize()
		{
			teamLogoTxet = GetChildUIComponent<Text>("Text_TeamLogo");
			gameLogoText = GetChildUIComponent<Text>("Text_GameLogo");

			if(teamLogoTxet && gameLogoText)
				EditorTool.Log("LogoUI is initialized.", LogType.Normal);
			else
				EditorTool.Log("LogoUI is failed to initialize.", LogType.Warning);
		}

		public override void Release() { }
	}
}
