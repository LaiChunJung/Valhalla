using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementEffects;

[AddComponentMenu("Utilities/HUDFPS")]
[RequireComponent(typeof(Text))]
public class FpsDisplay : MonoBehaviour
{
	public Text		text;
	public Color	fps_green;
	public Color	fps_yellow;
	public Color	fps_Red;
	public float	frequency = 0.5F;
	
	private float	accum = 0.0f;
	private int		frames = 0;

	private void Awake()
	{
		text = GetComponent<Text>();
	}

	private void Start()
	{		
		StartCoroutine( _FPS() );
	}

	IEnumerator<float> _FPS()
	{
		while (true)
		{
			accum += Time.timeScale / Time.deltaTime;
			frames++;

			float fps = Mathf.Floor(accum / (float)frames);

			text.text = "Fps " + fps.ToString ();

			text.color = (fps >= 60) ? fps_green : ((fps > 30) ?fps_yellow : fps_Red);

			accum = 0.0F;

			frames = 0;

			yield return Timing.WaitForSeconds(frequency);
		}
	}
}
