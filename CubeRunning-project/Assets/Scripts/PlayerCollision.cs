using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerCollision : MonoBehaviour {

	public PlayerMovment movement;
	public GameObject flyObjUI;

	int _flyTime = 50;

	void OnCollisionEnter(Collision otherCollider)
	{
		switch (otherCollider.collider.tag)
		{
			case "Obstacle":
				movement.enabled = false;
				RandomalLevel.UpdateScore((int)otherCollider.transform.position.z + StaticData.StepsCount * StaticData.StepSize);
				FindObjectOfType<GameManager>().EndGame();
				break;
		}
	}
	void OnTriggerEnter(Collider otherCollider)
	{
		Debug.Log("tr");
		switch (otherCollider.tag)
		{
			case "Trophy":
				if (movement.enabled) flyObjUI.SetActive(true);
				flyObjUI.GetComponentInChildren<Slider>().value = 1;
				movement.flyTime = _flyTime;
				break;
			case "Malicious":
				movement.speed *= 1.5f;
				break;
			case "Finish":
				FindObjectOfType<GameManager>().LevelComplete();
				break;
		}
	}
}
