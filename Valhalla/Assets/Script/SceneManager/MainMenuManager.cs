using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------
 * 
 * 主選單場景管理器.
 *	-2017/8/26 by Mahua.
 * 
 * ------------------------------*/

namespace Valhalla
{
	public class MainMenuManager : IMonoSingleton<MainMenuManager>
	{
		private void Awake()
		{
			// 初始化系統管理器.
			Core.Init();

			// 建立UICanvas.
			UITool.BuildUICanvas("MainMenuCanvas");

			// 初始化系統.
			InitSystems();
		}

		private void OnDestroy()
		{
			ReleaseSystems();
		}

		private void InitSystems()
		{
			// 新增UI系統.
			Core.AddSystem<LoadingUI>();
			Core.AddSystem<OptionUI>();
		}

		private void ReleaseSystems()
		{
			// 釋放UI系統.
			Core.RemoveSystem<LoadingUI>();
			Core.RemoveSystem<OptionUI>();
		}
	}
}