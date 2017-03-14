using UnityEngine;

namespace Behaviours.AIBehaviours {
	public class PigeonAIPatrolBehaviour : AIPatrolBehaviour {
        protected AIDetectFriendBehaviour _aiDetectFriendBehaviour;
		protected Animator _animator;
        protected override void OnEnable() {
            EventManager.ChangeStateEvent += ChangeStateEventHandler;
            EventManager.FriendDetectedEvent += FriendDetectedEventHandler;
        }
        protected override void OnDisable() {
            EventManager.ChangeStateEvent -= ChangeStateEventHandler;
            EventManager.FriendDetectedEvent -= FriendDetectedEventHandler;
        }
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _aiDetectFriendBehaviour = this.GetComponent<AIDetectFriendBehaviour>();
			_animator = this.GetComponent<Animator>();
        }
        protected override void FriendDetectedEventHandler(EventData eventData){
            if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                OnPatrolStop();
            }
        }
        protected override void OnPatrolStop() {
            _isMoving = false;
            if (_aiDetectEnemyBehaviour.GetTarget() == null) {
				_animator.SetBool("Idle", true);
				_animator.SetBool("Follow", false);
				_animator.SetBool("Patrol", false);
				EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Idle);
            }
            else {
				_animator.SetBool("Idle", false);
				_animator.SetBool("Follow", true);
				_animator.SetBool("Patrol", false);
				EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Follow);
            }
        }

    }
}