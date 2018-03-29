using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomalLevel : MonoBehaviour {

	public Transform[] obstacles;
	public Transform[] capsulse;
	public GameObject player;
	public GameObject StepFinishText;

	List<Transform> _obstacles;
	int _obstacleCount;
	int _maxLen;
	int _firstObst;
	int _emptyEndDist;

	List<Transform> _capsules;
	int _capsulesCount;


	
	void Start ()
	{
		Debug.Log("start step");
		#region init
		_emptyEndDist = 200;
		_maxLen = StaticData.StepSize + _emptyEndDist;
		_obstacleCount = _maxLen / (50 - (int)StaticData.Level);
		_capsulesCount = _obstacleCount / 3;
		_firstObst = 100;
		_obstacles = new List<Transform>();
		_capsules = new List<Transform>();
		#endregion
		GenerateObs();
		GenerateCapsulse();
	}

	void Update()
	{
		foreach (Transform item in _capsules)
		{
			Vector3 vector = new Vector3(0, ((int)Math.Floor(item.position.y) + 1) % 360, 0);
			item.Rotate(vector);
		}
		if(player.transform.position.z > _maxLen - _emptyEndDist)
		{
			Debug.Log("update position");
			StaticData.StepsCount++;
			StepCompleted();
			player.transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y, 5), player.transform.rotation);
		}
	}

	Vector3 FixPosition(Vector3 vector)
	{
		foreach (Transform obst in _obstacles)
		{
			if (Math.Abs(obst.position.z - vector.z) < 2)
			{
				if (vector.z > _maxLen) throw new OverflowException();
			}
		}
		foreach (Transform caps in _capsules)
		{
			if (Math.Abs(caps.position.z - vector.z) < 2)
			{
				if (vector.z > _maxLen) throw new OverflowException();
			}
		}
		return vector;
	}

	private int GetMaxX(String name)
	{
		switch (name)
		{
			case "Obstacle5":
				return 3;
			case "Obstacle3":
				return 4;
			case "Obstacle1":
				return 5;
			default:
				return 3;
		}

	}

	/// <summary>
	/// generate obstacles
	/// </summary>
	private void GenerateObs()
	{
		foreach (Transform item in obstacles)
		{
			int maxX = GetMaxX(item.name);
			for (int i = 0; i < _obstacleCount; i++)
			{
				try
				{
					Vector3 vector = new Vector3(UnityEngine.Random.Range(-3, maxX), 1,
						UnityEngine.Random.Range(_firstObst, _maxLen - _emptyEndDist));
					Transform newItem = Instantiate<Transform>(item);
					newItem.SetPositionAndRotation(FixPosition(vector), new Quaternion(0, 0, 0, 0));
					_obstacles.Add(newItem);
				}
				catch(OverflowException){}
			}
		}
	}

	private void GenerateCapsulse()
	{
		foreach (Transform item in capsulse)
		{
			for (int i = 0; i < _capsulesCount; i++)
			{
				try
				{
					Vector3 vector = new Vector3(UnityEngine.Random.Range(-3, 3), 1, UnityEngine.Random.Range(_firstObst, _maxLen - _emptyEndDist));
					Transform newItem = Instantiate<Transform>(item);
					newItem.SetPositionAndRotation(FixPosition(vector), new Quaternion(0, 0, 0, 1));
					_capsules.Add(newItem);
				}
				catch(OverflowException){}
			}
		}
	}

	public static void UpdateScore(int score){
		int level = (int)StaticData.Level;
		int currentMax;
		bool levelExist = StaticData.MaxScore.TryGetValue(level, out currentMax);
		if(!levelExist)
		{
			Debug.Log("newScore");
			StaticData.MaxScore.Add(level, score);
		}
		else
		{
			if (score > currentMax)
			{
				Debug.Log("betterScore");
				StaticData.MaxScore.Remove(level);
				StaticData.MaxScore.Add(level, score);
			}
		}
		int newScore;
		StaticData.MaxScore.TryGetValue(level, out newScore);
		Debug.Log(newScore);
	}

	private class OverFlowException : Exception
	{
		public OverFlowException(){}
	}

	void StepCompleted()
	{
		StepFinishText.SetActive(true);
		StepFinishText.GetComponentInChildren<Text>().text = "Complete Step " + StaticData.StepsCount;
		Debug.Log("complete step");
		Invoke("NextStep", 3);
	}

	void NextStep()
	{
		StepFinishText.SetActive(false);
	}

}


