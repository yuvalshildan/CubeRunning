using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject gameOverUI;
	public GameObject flyObjUI;
	public GameObject levelComplete;
	public GameObject bestScoreText;

	public int waitTime = 2;

	bool _gameHasEnded = false;

	public void LevelComplete()
	{
		if (!_gameHasEnded)
		{
			Debug.Log("complete");
			_gameHasEnded = true;
			levelComplete.SetActive(true);
			flyObjUI.SetActive(false);
		}
	}

	public void EndGame()
	{
		if (!_gameHasEnded)
		{
			Debug.Log("end");
			_gameHasEnded = true;
			gameOverUI.SetActive(true);
			flyObjUI.SetActive(false);
			if(bestScoreText) bestScoreText.SetActive(false);
			Invoke("Restart", waitTime);
		}
	}

	public void Restart()
	{
		StaticData.StepsCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
