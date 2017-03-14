using UnityEngine;

namespace Behaviours.CharacterBehaviours {
    public class VoiceOverBehaviour : AbstractBehaviour {
        protected AudioSource _audioSource;

        protected AudioClip _currentSource;
        protected AudioClip _nextSource;

        public AudioClip[] _pigeonTalk;
        public AudioClip[] _bitchinPigeon;
        public AudioClip[] _penquinTaunts;
        public AudioClip[] _edisonTaunts;

        [SerializeField]
        protected LayerMask _characterLayers;

        [SerializeField]
        protected float _nextSFXTime = 0;

        protected float _nextLimit = 0;
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _audioSource = this.GetComponent<AudioSource>();
        }
        protected virtual void Update() {
            if(_nextSource != null) {
                if(!_audioSource.isPlaying) {
                    OnPlaySound(_nextSource);
                }
            }
        }
        protected virtual void OnPlaySound(AudioClip audioClip) {
            if(_audioSource.isPlaying) {
                _nextSource = audioClip;
            }
            else {
                _currentSource = audioClip;
                _nextSource = null;
                //Debug.Log("Playing SFX");
				//_audioSource.PlayOneShot(_currentSource);
				_audioSource.clip = _currentSource;
				_audioSource.Play();
            }
        }
        protected virtual void PlaySoundEventHandler(EventData eventData, ECharacter characterType) {
            if(this.BehaviourObservesEvent(eventData)) {
                //Debug.Log("SOUND EVENT!");
                int randomSFX = -1;
                AudioClip[] temp = null;
                Debug.Log("Character Type!" + characterType);
                if (characterType == ECharacter.Pigeon && _pigeonTalk.GetLength(0) != 0) {
                     randomSFX = Random.Range(0, _pigeonTalk.GetLength(0) - 1);
                     temp = _pigeonTalk; 
                }
                if(characterType == ECharacter.BitchinPigeon && _bitchinPigeon.GetLength(0) != 0) {
                    randomSFX = Random.Range(0, _bitchinPigeon.GetLength(0) - 1);
                    temp = _bitchinPigeon;
                }
                else if(characterType == ECharacter.Penquin && _penquinTaunts.GetLength(0) != 0) {
                    randomSFX = Random.Range(0, _penquinTaunts.GetLength(0) - 1);
                    temp = _penquinTaunts;
                }
                else if(characterType == ECharacter.Edison && _edisonTaunts.GetLength(0) != 0) {
                    randomSFX = Random.Range(0, _edisonTaunts.GetLength(0) - 1);
                    temp = _edisonTaunts;
                }
                if(randomSFX > -1 && temp != null) {
                    //Debug.Log("Temp Count: " + temp.GetLength(0));
                    OnPlaySound(temp[randomSFX]);
                }
            }
        }
        protected virtual void OnTriggerEnter(Collider collider) {
            if (Tools.Tools.GetIsLayerInLayerMask(collider.gameObject, _characterLayers)) {
                if (Time.time > _nextLimit) {
                    _nextLimit = Time.time + _nextSFXTime;
                    PlaySoundEventHandler(new EventData(this.gameObject, true), collider.gameObject.GetComponent<CharacterTypeBehaviour>().CharacterType);
                }
            }
        }
        protected virtual void OnTriggerStay(Collider collider) {
            OnTriggerEnter(collider);
        }
    }
}