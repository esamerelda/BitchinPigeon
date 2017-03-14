using UnityEngine;

namespace Behaviours.CharacterBehaviours {
    public class HealthBehaviour : AbstractBehaviour {
		protected Animator _anim;
		protected AudioController _audio;
		protected const int MINIMUM_HEALTH = 0;
        protected const int MAXIMUM_HEALTH = 100;

		protected override void SetInitialReferences()
		{
			base.SetInitialReferences();
			_anim = this.GetComponent<Animator>();
			_audio = this.GetComponent<AudioController>();
		}

		[SerializeField][Range(MINIMUM_HEALTH, MAXIMUM_HEALTH)]
        protected float _health = 100;

        protected override void OnEnable() {
            base.OnEnable();
            EventManager.HealEvent += HealEventHandler;
            EventManager.DamageEvent += DamageEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.HealEvent -= HealEventHandler;
            EventManager.DamageEvent -= DamageEventHandler;
        }
        protected virtual void HealEventHandler(EventData eventData, float health) {
            if(this.BehaviourObservesEvent(eventData)) {
                OnHeal(health);
            }
        }
        protected virtual void DamageEventHandler(EventData eventData, float damage) {
            if(this.BehaviourObservesEvent(eventData)) {
                OnDamage(damage);
            }
        }
        protected virtual void OnHeal(float health) {
            _health = Mathf.Clamp(_health + health, MINIMUM_HEALTH, MAXIMUM_HEALTH);
        }
        protected virtual void OnDamage(float damage) {
			_anim.SetBool("Chase", false);
			_anim.SetBool("Idle", false);
			_anim.SetBool("Patrol", false);
			_anim.SetTrigger("Hit");
			_audio.PlaySoundOnce(_audio.HitClip);
            _health = Mathf.Clamp(_health - damage, MINIMUM_HEALTH, MAXIMUM_HEALTH);
            if(_health == MINIMUM_HEALTH) {
				_anim.SetTrigger("Die");
				//_audio.PlaySoundOnce(_audio.DieClip);
                EventManager.InvokeDeathEvent(new EventData(this.gameObject, true));
            }
        }
    }
}