using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public GameObject gameObj;
	public Text level;

	string INFINITE_SCENE = "RandomalLevel";
	string FIRST_SCENE = "Level1";

	void Start()
	{
		gameObj.GetComponent<Slider>().value = ((float) StaticData.Level)/10f - 0.1f;
	}

	void Update()
	{
		level.text = ((gameObj.GetComponent<Slider>().value + 0.1f) * 10f).ToString("0");
	}

	public void StartGame()
	{
		StaticData.Level = (gameObj.GetComponent<Slider>().value + 0.1f) * 10f;
		Debug.Log(StaticData.Level);
		SceneManager.LoadScene(FIRST_SCENE);
	}

	public void InfiniteLevel()
	{
		StaticData.Level = (gameObj.GetComponent<Slider>().value + 0.1f) * 10f;
		Debug.Log(StaticData.Level);
		SceneManager.LoadScene(INFINITE_SCENE);
	}
}
