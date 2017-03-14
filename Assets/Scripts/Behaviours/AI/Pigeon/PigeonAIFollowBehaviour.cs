using UnityEngine;

namespace Behaviours.AIBehaviours {
	public class PigeonAIFollowBehaviour : AbstractAIStateBehaviour {
        protected AIDetectFriendBehaviour _aiDetectFriendBehaviour;
		protected Animator _animator;
        bool _isCollected = false;

        protected override void OnEnable() {
			base.OnEnable();
			EventManager.ChangeStateEvent += ChangeStateEventHandler;
            EventManager.FriendDetectedEvent += FriendDetectedEventHandler;
            EventManager.FriendLostEvent += FriendLostEventHandler;
		}
		protected override void OnDisable() {
			base.OnDisable();
			EventManager.ChangeStateEvent -= ChangeStateEventHandler;
            EventManager.FriendDetectedEvent -= FriendDetectedEventHandler;
            EventManager.FriendLostEvent -= FriendLostEventHandler;
        }

        protected override void Awake() {
            base.Awake();
			_animator.SetBool("Idle", false);
			_animator.SetBool("Follow", true);
			_animator.SetBool("Patrol", false);
			SetStateBehaviour(EAIState.Follow);
        }
        protected virtual void Update() {
            if(_stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                OnFollowStart();
            }
        }
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _aiDetectFriendBehaviour = this.GetComponent<AIDetectFriendBehaviour>();
			_animator = this.GetComponent<Animator>();
        }
        protected override void ChangeStateEventHandler(EventData eventData, EAIState state) {
            if(this.BehaviourObservesEvent(eventData) && state == _stateBehaviour) {
                base.ChangeStateEventHandler(eventData, state);
                OnFollowStart();
            }
        }
        protected override void FriendDetectedEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                OnFollowStart();
            }
        }
        protected virtual void FriendLostEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                //OnFollowEnd();
            }
        }
        protected void OnFollowStart() {
            if (_aiDetectFriendBehaviour.GetTarget() != null) {
                //Debug.Log("Following...");
                if (!_isCollected) {
                    _isCollected = true;
                    GameManager._pigeonsCollected++;
                }
				_navMeshAgent.SetDestination(_aiDetectFriendBehaviour.GetTarget().position);
            }
        }
        protected virtual void OnFollowEnd() {
			_animator.SetBool("Idle", false);
			_animator.SetBool("Follow", false);
			_animator.SetBool("Patrol", true);
			EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Patrol);
        }
	}
}