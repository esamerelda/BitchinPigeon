using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScreen : UIBehaviour {

	public AudioClip sound;
	public AudioSource source;
    public bool _isLoading = false;
    public bool _isQuiting = false;
    public void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlaySound()
	{
		source.PlayOneShot(sound);
	}
    public void LoadScene(string sceneName) {
        if (!_isLoading) {
            _isLoading = true;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
    public void Quit() {
        if(!_isQuiting) {
            _isQuiting = true;
            Application.Quit();
        }
    }
}