using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovment : MonoBehaviour {

	public Rigidbody rb;
	public Text startText;
	public Text bestScoreText;
	public GameObject flyObjUI;

	public float speed;
	public float vertical;
	public float horizon;

	public int flyTime;


	float _flyFull;
	bool _gameHasStarted;
	bool _gameHasEnded;

	void Start()
	{
		speed = (float) (1000 + StaticData.Level * 400);
		vertical = (float) (10 + StaticData.Level * 6);
		horizon = 50f;
		flyTime = 0;
		_flyFull = 50f;
		_gameHasStarted = false;
		_gameHasEnded = false;
	}

	// Update is called once per frame
	void Update()
	{
		//update best score
		if(bestScoreText != null)
		{
			UpdateBestScore();
		}
		//quit key
		if (Input.GetKey(KeyCode.Q))
		{
			SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
		}
		//start key
		if (Input.GetKey(KeyCode.S) && !_gameHasStarted)
		{
			Debug.Log("s");
			_gameHasStarted = true;
			startText.text = "0";
			
		}
		//only check if game has started
		if (!_gameHasStarted) return;
		//right key
		if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(vertical * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}
		//left key
		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(-vertical * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.W) && flyTime > 0)
		{
			rb.AddForce(0, horizon * Time.deltaTime, 0, ForceMode.VelocityChange);
			if (--flyTime == 0) flyObjUI.SetActive(false);
			Debug.Log(flyObjUI.GetComponentInChildren<Slider>());
			flyObjUI.GetComponentInChildren<Slider>().value = (flyTime / _flyFull);
			Debug.Log(flyTime);
		}
		if (rb.position.y < 0 && !_gameHasEnded)
		{
			_gameHasEnded = true;
			Debug.Log(StaticData.StepsCount);
			RandomalLevel.UpdateScore((int)rb.position.z + StaticData.StepsCount * StaticData.StepSize);
			FindObjectOfType<GameManager>().EndGame();
		}
	}

	void FixedUpdate () {
		if (_gameHasStarted)
		{
			rb.AddForce(0, 0, speed * Time.deltaTime * rb.mass);	
		}
	}

	void UpdateBestScore()
	{
		int currBest;
		int currPos = (int)rb.position.z + StaticData.StepsCount * StaticData.StepSize;
		string newText = "Best Score: ";
		if (StaticData.MaxScore.TryGetValue((int)StaticData.Level, out currBest))
		{
			if (currPos > currBest)
			{
				newText += currPos;
				bestScoreText.color = new Color(51, 102, 0);
			}
			else
			{
				newText += currBest;
			}
		}
		else
		{
			newText += currPos;
			bestScoreText.color = new Color(51, 102, 0);
		}
		bestScoreText.text = newText;
	}
}
