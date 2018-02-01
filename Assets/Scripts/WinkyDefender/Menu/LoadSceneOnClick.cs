using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void	LoadByIndex (int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void LoadByName (string name) {
		SceneManager.LoadScene (name);
	}

	public void ReloadScene () {
//		Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void LoadNextScene () {
		int index = SceneManager.GetActiveScene ().buildIndex;
		index++;
		Time.timeScale = 1;

		if (index > 1 && index <= 9)
			LoadByIndex (index);
		else
			LoadByIndex (1);
	}
}
