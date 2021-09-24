using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		if(Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
		SceneManager.LoadScene (sceneName);
	}
	public void Exit()
	{
		Application.Quit ();
	}
}