using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class ParseData : MonoBehaviour {

	public TextAsset text;

	void Start () {
		if (StaticData.IsParsed) return;
		StaticData.InitMaxScore();
	}
}
