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

			// 初始化所有UI系統.
			InitUI();

			// 初始化所有Game系統.
			InitGameSystem();
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

		private void OnDestroy()
		{
			ReleaseUI();
			ReleaseSystem();
		}

		// 初始化所有Game系統.
		private void InitGameSystem()
		{
			MainManager.AddMonoSystem<InputSystem>();
		}

		// 初始化所有UI系統. (在Awake()執行)
		private void InitUI()
		{
			// 建立StartCanvas.
			UITool.BuildUICanvas("StartCanvas");

			// 新增系統 - LogoUI.
			MainManager.AddSystem<LogoUI>();
		}

		// 移除系統. (在OnDestroy()執行)
		private void ReleaseSystem()
		{
			// 移除系統 - InputSystem.
			MainManager.RemoveSystem<InputSystem>();
		}

		// 釋放所有UI系統. (在OnDestroy()執行)
		private void ReleaseUI()
		{
			// 移除系統 - LogoUI.
			MainManager.RemoveSystem<LogoUI>();
		}
	}
}
