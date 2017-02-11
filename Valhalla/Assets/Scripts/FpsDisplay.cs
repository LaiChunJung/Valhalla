using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Utilities/HUDFPS")]
public class FpsDisplay : MonoBehaviour
{
	private static FpsDisplay _instance = null;
	public static FpsDisplay Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<FpsDisplay>();
				if (_instance == null)
				{
					Debug.LogWarning("Fps-UiText is not exist.");
				}
			}

			return _instance;
		}
	}
	
	public float frequency = 0.5F;

	private Text text;
	private float accum = 0f;
	private int frames = 0;
	private Color color = Color.green;

	void Awake()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
	}

	void Start()
	{
		text = GetComponent<Text>();
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
