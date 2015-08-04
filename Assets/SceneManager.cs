using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
	public static SceneManager GetInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<SceneManager>();
		}

		return instance;
	}

	static SceneManager instance;

	CanvasGroup overlayCanvas;

	void Awake()
	{
		overlayCanvas = transform.FindChild("Canvas/Image").GetComponent<CanvasGroup>();
		DontDestroyOnLoad(this.gameObject);
	}

	public void LoadScene(string sceneName)
	{
		Debug.Log("LoadScene");
		StartCoroutine(FadeInOut(sceneName));
	}

	IEnumerator FadeInOut(string sceneName)
	{
		overlayCanvas.gameObject.SetActive(true);
		overlayCanvas.alpha = 0;

		float time = 0;
		float duration = 1f;

		while(time <= duration)
		{
			overlayCanvas.alpha = Mathf.Lerp(0,1,time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		time = 0;
		overlayCanvas.alpha = 1;
		Application.LoadLevel(sceneName);

		while(time <= duration)
		{
			overlayCanvas.alpha = Mathf.Lerp(1,0,time/duration);
			time += Time.deltaTime;
			yield return null;
		}

		overlayCanvas.alpha = 0;
		overlayCanvas.gameObject.SetActive(false);

	}
}
