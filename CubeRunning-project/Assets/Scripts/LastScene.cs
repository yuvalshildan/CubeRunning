using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class LastScene : MonoBehaviour {

	const string SCORES_PATH = "BestRun.txt";
	public void QuitApp()
	{
		string path = Path.Combine(Application.persistentDataPath, SCORES_PATH);
		Debug.Log("path: " + path);
		try
		{
			StreamWriter sw = new StreamWriter(path);
			foreach (var key in StaticData.MaxScore)
			{
				sw.WriteLine(key.Value);
			}
			sw.Flush();
		}
		catch(Exception e)
		{
			StaticData.ReportError(e.Message);
		}

		Debug.Log("Quit");
		Application.Quit();
	}

	public void RestartApp()
	{
		Debug.Log("Restart");
		SceneManager.LoadScene(0);
	}
}
