using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Text _pigeonsCollected;
	
	// Update is called once per frame
	void Update () {
        _pigeonsCollected.text = GameManager._pigeonsCollected.ToString() + " / " + GameManager._pigeonsToCollect.ToString();
    }
}
