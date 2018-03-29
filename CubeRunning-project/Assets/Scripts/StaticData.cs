using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public static class StaticData {

	const string SCORES_PATH = "BestRun.txt";

	public static double Level;

	public static Dictionary<int, int> MaxScore = new Dictionary<int,int>();

	//steps of StepSize in the infinity level
	public static int StepsCount = 0;

	public static int StepSize = 1000;

	public static bool IsParsed = false;
 
	public static void ReportError(string error)
	{
		Debug.Log(DateTime.Now + ": " + error);
	}

	static void InitEmptyMaxScore()
	{
		Debug.Log("parse empty");
		for (int i = 1; i < 11; i++)
		{
			StaticData.MaxScore.Add(i, 0);
		}
		StaticData.IsParsed = true;
	}

	public static void InitMaxScore()
	{
		string path = Path.Combine(Application.persistentDataPath, SCORES_PATH);
		Debug.Log("path: " + path);

		if (!File.Exists(path)) InitEmptyMaxScore();
		else
		{
			int i = 1;
			using (StreamReader sr = new StreamReader(path))
			{
				Debug.Log("parse file: " + path);
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					int score;
					if (!Int32.TryParse(line, out score))
					{
						Debug.Log("Failure: " + line);
						return;
					}
					StaticData.MaxScore.Add(i++, score);
				}
			}
			StaticData.IsParsed = true;
		}
	}
}
