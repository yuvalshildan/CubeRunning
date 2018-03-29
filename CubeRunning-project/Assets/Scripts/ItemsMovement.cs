using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemsMovement : MonoBehaviour {

	public Transform[] preFabs;
	List<Transform> _items;
	float _maxLen = 600;
	int _itemsCount = 3;
	void Start()
	{
		_items = new List<Transform>();
		foreach (Transform item in preFabs)
		{
			for (int i = 0; i < _itemsCount; i++)
			{
				Vector3 vector = new Vector3(UnityEngine.Random.Range(-3, 3), 1, UnityEngine.Random.Range(5, _maxLen));
				Transform newItem = Instantiate<Transform>(item);
				newItem.SetPositionAndRotation(vector, new Quaternion(0, 0, 0, 1));
				_items.Add(newItem);
			}
		}
	}

	void Update () {
		foreach (Transform item in _items)
		{
			Vector3 vector = new Vector3 (0, ((int) Math.Floor(item.position.y) + 1) % 360, 0);
			item.Rotate(vector);
		}
	}
}
