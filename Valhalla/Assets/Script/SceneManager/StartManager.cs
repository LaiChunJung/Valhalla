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
			MainManager.Init();

			// 建立StartCanvas.
			UITool.BuildUICanvas("StartCanvas");

			// 初始化所有系統.
			InitSystem();
		}

		private void Start()
		{
			// 播放Logo.
			MainManager.GetSystem<LogoUI>().StartLogo(() =>
			{
				// 切換場景.
				ValhallaApp.LoadScene("MainMenu");
			});
		}

		private void Update()
		{
			MainManager.SystemUpdate();
		}

		// 新增系統.
		private void InitSystem()
		{
			// 新增Game系統.
			MainManager.AddSystemMono<InputSystem>();

			// 新增UI系統.
			MainManager.AddSystem<LogoUI>();
		}

		// 移除系統.
		private void ReleaseSystem()
		{
			// 移除Game系統.
			MainManager.RemoveSystem<InputSystem>();

			// 移除UI系統.
			MainManager.RemoveSystem<LogoUI>();
		}
	}
}
