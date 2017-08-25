using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			MainManager.Init();

			UITool.BuildUICanvas("MainMenuCanvas");

			InitSystems();

			SceneManager.sceneUnloaded += ReleaseSystems;
		}

		private void InitSystems()
		{
			// 新增UI系統.
			MainManager.AddSystem<LoadingUI>();
			MainManager.AddSystem<OptionUI>();
		}

		private void ReleaseSystems(Scene scene)
		{
			// 新增UI系統.
			MainManager.RemoveSystem<LoadingUI>();
			MainManager.RemoveSystem<OptionUI>();
		}
	}
}