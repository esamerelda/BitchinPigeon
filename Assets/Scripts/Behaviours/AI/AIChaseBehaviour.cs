using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.AIBehaviours {
	public class AIChaseBehaviour : AbstractAIStateBehaviour {
		protected Animator _anim;
		protected AudioController _audio;
		bool _isChasing = false;
        protected override void Awake() {
            base.Awake();
            SetStateBehaviour(EAIState.Chase);
        }
        protected override void OnEnable() {
            base.OnEnable();
            EventManager.EnemyLostEvent += EnemyLostEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.EnemyLostEvent -= EnemyLostEventHandler;
        }

		protected override void SetInitialReferences()
		{
			base.SetInitialReferences();
			_anim = this.GetComponent<Animator>();
			_audio = this.GetComponent<AudioController>();
		}
		protected override void ChangeStateEventHandler(EventData eventData, EAIState state) {
            if(this.BehaviourObservesEvent(eventData) && state == _stateBehaviour) {
                base.ChangeStateEventHandler(eventData, state);
                OnChase();
            }
        }
        protected override void EnemyDectedEventHandler(EventData eventData) {
           if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                //Debug.Log("Enemy Detected");
                OnChase();
           }
        }
        protected virtual void EnemyLostEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                OnChaseEnd();
            }
        }
        protected virtual void OnChase() {
            //Debug.Log("Calling On CHase!");
            if (_aiDetectEnemyBehaviour.GetTarget() != null) {
                _navMeshAgent.SetDestination(_aiDetectEnemyBehaviour.GetTarget().position);

                //Debug.Log("Target Position: " + _aiDetectEnemyBehaviour.GetTarget().position);
            }
            else
            {
                //Debug.Log("Target Is Null!");
            }
        }
        protected virtual void OnChaseEnd() {
            _isChasing = false;
			_anim.SetBool("Chase", false);
			_anim.SetBool("Idle", false);
			_anim.SetBool("Patrol", true);
			_audio.StartLoop(_audio.WalkLoop);
            EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Patrol);
        }
    }
}