using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MovementEffects;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/*--------------------------------
 * 
 * 起始畫面場景管理器.
 *	-2017/8/26 by Mahua.
 * 
 * ------------------------------*/

namespace Valhalla
{
	public class StartManager : IMonoSingleton<StartManager>
	{
		private void Awake()
		{
			// 初始化系統管理器.
			Core.Init();

			// 建立StartCanvas.
			UITool.BuildUICanvas("StartCanvas");

			// 初始化所有系統.
			InitSystems();
		}

		private void Start()
		{
			// 播放Logo.
			Core.GetSystem<LogoUI>().StartLogo(() =>
			{
				// 切換場景.
				ValhallaApp.LoadScene("MainMenu");
			});
		}

		private void Update()
		{
			Core.SystemUpdate();
		}

		private void OnDestroy()
		{
			ReleaseSystems();
		}

		// 新增系統.
		private void InitSystems()
		{
			// 新增Game系統.
			Core.AddSystem<InputSystem>();

			// 新增UI系統.
			Core.AddSystem<LogoUI>();
			Core.AddSystem<LoadingUI>();
		}

		// 移除系統.
		private void ReleaseSystems()
		{
			// 移除Game系統.
			Core.RemoveSystem<InputSystem>();

			// 移除UI系統.
			Core.RemoveSystem<LogoUI>();
			Core.RemoveSystem<LoadingUI>();
		}
	}
}
