using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*--------------------------------
 * 
 * 主選單/選項UI系統.
 *	-2017/8/26 by Mahua.
 * 
 * ------------------------------*/

namespace Valhalla
{
	public class OptionUI : IUserInterface<OptionUI>
	{
		private Button buttonStart;

		public OptionUI() : base()
		{
			buttonStart = UITool.GetChildUIComponent<Button>(m_Root, "Button Start");
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
