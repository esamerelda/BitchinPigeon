using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour {

	public void DisablePopUp(Canvas screen)
	{
		screen.enabled = false;
	}

	public void EnablePopUp(Canvas screen)
	{
		screen.enabled = true;
	}

	public void QuitGame()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}


}
