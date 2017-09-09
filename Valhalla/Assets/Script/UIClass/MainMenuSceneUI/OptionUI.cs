using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*--------------------------------
 * 
 * 主選單/選項UI系統.
 *	-2017/8/26 by Mahua.
 *	
 *	說明:
 *	OptionUI系統使用在MainMenu場景的MainMenuManager程式中，
 *	主要是用來做主選單UI操作。
 * 
 * ------------------------------*/

namespace Valhalla
{
	public class OptionUI : IUserInterface<OptionUI>
	{
		private Button buttonStart;

		public OptionUI() : base("OptionUI")
		{
			buttonStart = UITool.GetChildUIComponent<Button>(m_Root, "Button Start");

			// 對Start按鈕加入按鈕事件.
			buttonStart.onClick.AddListener(OnButtonStart);
		}

		public override void Initialize() { }

		public override void Release() { }

		private void OnButtonStart()
		{
			buttonStart.gameObject.SetActive(false);
			ValhallaApp.LoadScene("Battle");
		}
	}
}
