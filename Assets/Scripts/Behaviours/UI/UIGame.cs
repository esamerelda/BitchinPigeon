using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGame : UIBehaviour {

	private bool isPaused;
    private bool _isLoading = false;
	[SerializeField]
	protected Canvas _pauseCanvas;

	void Awake() {
		GameManager._pigeonsCollected = 0;
        Time.timeScale = 1;
        _pauseCanvas.enabled = false;
	}

	void Start()
	{
		isPaused = false;
	}

	void Update()
	{
		if (Input.GetButtonDown("Esc")) {
			isPaused = !isPaused;
			if (isPaused){
				Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				_pauseCanvas.enabled = true;
			}
			else {
				Time.timeScale = 1;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				_pauseCanvas.enabled = false;
			}
		}
	}
    public void LoadMenu(string menuName) {
        if(!_isLoading) {
            _isLoading = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(menuName, LoadSceneMode.Single);
        }
    }
}
