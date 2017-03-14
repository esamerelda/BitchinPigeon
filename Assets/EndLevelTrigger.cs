using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour {
    public float endTime = 20f;
    protected virtual void OnTriggerEnter(Collider collider) {
        if(Tools.Tools.GetIsOnLayer(collider.gameObject, "Player")) {
            StartCoroutine(EndLevel());
        }
    }
    IEnumerator EndLevel() {
        yield return new WaitForSeconds(endTime);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}
