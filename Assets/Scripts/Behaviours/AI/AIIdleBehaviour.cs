using UnityEngine;

namespace Behaviours.AIBehaviours {
	public class AIIdleBehaviour : AbstractAIStateBehaviour {

		protected Animator _anim;
		protected AudioController _audio;
		protected const float MAXIMUM_WAIT_TIME = 5.0f;
		protected const float MINIMUM_WAIT_TIME = 1.0f;

        [SerializeField][Range(MINIMUM_WAIT_TIME, MAXIMUM_WAIT_TIME)]
		protected float _waitTime = 2f;

        protected float _stopTime;

        protected override void Awake() {
            base.Awake();
            SetStateBehaviour(EAIState.Idle);
        }

		protected override void SetInitialReferences()
		{
			base.SetInitialReferences();
			_anim = this.GetComponent<Animator>();
			_audio = this.GetComponent<AudioController>();
		}

		protected override void SetInitialValues() {
            base.SetInitialValues();
			
        }
        protected virtual void Update() {
            if(_aiStateManagerBehaviour.GetAIState() == _stateBehaviour) {
                if(Time.time > _stopTime) {
                    OnIdleEnd();
                }
            }
        }
        protected override void ChangeStateEventHandler(EventData eventData, EAIState state) {
            if(this.BehaviourObservesEvent(eventData) && state == _stateBehaviour) {
                base.ChangeStateEventHandler(eventData, state);
                OnIdleStart();
            }
        }
        protected override void EnemyDectedEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && _aiStateManagerBehaviour.GetAIState() == _stateBehaviour) {
                OnIdleEnd();
            }
        }
        protected virtual void OnIdleStart() {
            _stopTime = Time.time + _waitTime;
            _navMeshAgent.SetDestination(this.transform.position);
        }
        protected virtual void OnIdleEnd() {
            if (_aiDetectEnemyBehaviour.GetTarget() == null) {
				_anim.SetBool("Patrol", true);
				_anim.SetBool("Idle", false);
				_audio.StartLoop(_audio.WalkLoop);
                EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Patrol);
            }
            else {
				_anim.SetBool("Chase", true);
				_anim.SetBool("Idle", false);
				_anim.SetBool("Patrol", false);
				_audio.StartLoop(_audio.ChaseLoop);
                EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Chase);
            }
        }
    }
}