using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MovementEffects;

namespace Valhalla
{
	public class StartManager : IMonoSingleton<StartManager>
	{
		private void Awake()
		{
			// 初始化系統管理器.
			SystemManager.Init();

			// 初始化所有UI系統.
			InitUI();
		}

		private void Start()
		{
			// 播放Logo.
			/*SystemManager.GetSystem<LogoUI>().StartLogo(() =>
			{
				// 切換場景.
				ValhallaApp.LoadScene("MainMenu");
			});*/
		}

		private void Update()
		{
			SystemManager.UpdateGameSystem();
		}

		private void OnDestroy()
		{
			ReleaseUI();
		}

		private void InitUI()
		{
			UITool.BuildUICanvas("StartCanvas");

			//SystemManager.AddSystem<LogoUI>();
		}

		private void ReleaseUI()
		{
			//SystemManager.RemoveSystem<LogoUI>();
		}
	}
}
