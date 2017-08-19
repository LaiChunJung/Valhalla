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

		public LogoUI(GameObject root) : base(root) { }

		public override void Initialize()
		{
			base.Initialize();

			UITool.BuildUICanvas("StartCanvas");
		}
	}
}
