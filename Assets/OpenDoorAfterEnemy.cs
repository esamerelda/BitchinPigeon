using UnityEngine;

public class OpenDoorAfterEnemy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        this.gameObject.SetActive(!GameManager._edisonDead);
	}
}