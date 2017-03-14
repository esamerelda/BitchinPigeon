using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours {
    public class OpenDoorBehaviour : MonoBehaviour {
        protected virtual void Update() {
            if(GameManager._pigeonsCollected >= GameManager._pigeonsToCollect) {
                this.gameObject.SetActive(false);
            }
        }
    }
}