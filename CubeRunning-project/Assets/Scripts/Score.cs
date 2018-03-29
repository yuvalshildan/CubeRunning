using UnityEngine;
using UnityEngine.UI;
using System;


public class Score : MonoBehaviour {

	public Text scoreText;
	public Transform player;
	string _startMessage = "Press \"s\" to START";
	
	// Update is called once per frame
	void Update () {
		scoreText.text = (Math.Floor(player.position.z) == -1) ? _startMessage : (player.position.z + StaticData.StepsCount * StaticData.StepSize).ToString("0");
	}
}
