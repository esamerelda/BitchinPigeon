using UnityEngine;

namespace Behaviours.CharacterBehaviours {
    public class DeathBehaviour : AbstractBehaviour {
		[SerializeField]
        protected AudioSource _audioSource;

        [SerializeField]
        protected GameObject _explosion;

        [SerializeField]
        protected AudioClip _explosionSound;
		[SerializeField]
		protected AudioClip _guitarSound;

        protected bool _isDying = false;

        protected override void OnEnable() {
            base.OnEnable();
            EventManager.DeathEvent += DeathEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.DeathEvent -= DeathEventHandler;
        }
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
        }

        protected virtual void DeathEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && !_isDying) {
                OnDeath();
            }
        }
        protected virtual void OnDeath() {
            _isDying = true;
            if(Tools.Tools.GetIsOnLayer(this.gameObject, "Edison")) {
                GameManager._edisonDead = true;
            }
            ExplosionSFX();
            Explosion();
			//Destroy(this.gameObject.GetComponent<renderer>)
            Destroy(this.gameObject, _guitarSound.length);

        }
        protected virtual void ExplosionSFX() {
            _audioSource.PlayOneShot(_explosionSound);
			_audioSource.PlayOneShot(_guitarSound);
        }
        protected virtual void Explosion() {
            GameObject partical = Instantiate(_explosion, this.transform.position + _explosion.transform.position, this.transform.rotation) as GameObject;
            Destroy(partical, 5f);
        }
    }
}