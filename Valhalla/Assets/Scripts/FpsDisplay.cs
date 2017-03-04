using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Utilities/HUDFPS")]
public class FpsDisplay : MonoBehaviour
{
	public float	frequency = 0.5F;

	private Text	text;
	private Color	color = Color.green;
	private float	accum = 0f;
	private int		frames = 0;

	void Start()
	{
		text = GetComponent<Text>();

		Manager.Instance.UpdateDel += Launch;

		StartCoroutine(FPS());
	}

	public void Launch()
	{
		accum += Time.timeScale / Time.deltaTime;
		frames ++;
	}

	IEnumerator FPS()
	{
		while (true)
		{
			float fps = Mathf.Floor(accum / frames);
			text.text = "Fps " + fps.ToString ();
			
			color = (fps >= 60) ? Color.green : ((fps > 30) ? Color.yellow : Color.red);
			text.color = color;

			accum = 0.0F;
			frames = 0;

			yield return new WaitForSeconds(frequency);
		}
	}
}
