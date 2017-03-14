using UnityEngine;

namespace Behaviours.BassGuiatarBehaviours {
    public class DestroyBulletBehaviour : AbstractBehaviour {
        protected const float MINIMUM_TIME = 0.1f;
        protected const float MAXIMUM_TIME = 5.0f;

        [SerializeField]
        protected float _destoryTime = 0.35f;

        protected override void Awake() {
            base.Awake();
            Destroy(this.gameObject, _destoryTime);
        }
        protected virtual void OnCollisionEnter(Collision collision) {
            Destroy(this.gameObject);
        }
    }
}