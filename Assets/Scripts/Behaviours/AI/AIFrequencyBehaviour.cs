using UnityEngine;

namespace Behaviours.AIBehaviours {
    public class AIFrequencyBehaviour : AbstractBehaviour {
        protected const float MINIMUM_PITCH = 0.79f;
        protected const float MAXIMUM_PITCH = 1.88f;
        protected const float MINIMUM_RANGE = 0.01f;
        protected const float MAXIMUM_RANGE = 0.50f;

		[SerializeField]
		protected AudioSource _audioSource;

		[SerializeField]
        protected AudioClip _bassSFX;

        [SerializeField][Range(MINIMUM_PITCH, MAXIMUM_PITCH)]
        protected float _pitch = 1;

        [SerializeField][Range(MINIMUM_RANGE, MAXIMUM_RANGE)]
        protected float _pitchRange = 0.2f;

        [SerializeField]
        protected float _pitchWaitTime = 5f;

        protected float _nextPitchPlay = 0;

        protected override void OnEnable() {
            base.OnEnable();
            EventManager.FrequencyEvent += FrequencyEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.FrequencyEvent -= FrequencyEventHandler;
        }
        protected virtual void Update() {
            if(Time.time > _nextPitchPlay) {
                _nextPitchPlay = Time.time + _pitchWaitTime;
                _audioSource.pitch = _pitch;
				//_audioSource.PlayOneShot(_bassSFX);
				_audioSource.clip = _bassSFX;
				_audioSource.Play();
            }
        }
        protected override void SetInitialValues() {
            base.SetInitialValues();
            _pitch = Random.Range(MINIMUM_PITCH, MAXIMUM_PITCH);
        }
        protected virtual void FrequencyEventHandler(EventData eventData, float pitch, float damage) {
            if(this.BehaviourObservesEvent(eventData)) {
                OnFrequencyReceived(pitch, damage);
            }
        }
        protected virtual void OnFrequencyReceived(float pitch, float damage) {
            if(pitch <= _pitch + _pitchRange && pitch >= _pitch - _pitchRange) {
                EventManager.InvokeDamageEvent(new EventData(this.gameObject, true), damage);
            }
        }
    }
}