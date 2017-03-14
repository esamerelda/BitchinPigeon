using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.AIBehaviours {
	public class AIPatrolBehaviour : AbstractAIStateBehaviour {

		protected Animator _anim;
		protected AudioController _audio;
		protected const float MINIMUM_WAIT_TIME = 0.5f;
        protected const float MAXIMUM_WAIT_TIME = 5.0f;
        protected const float MAXIMUM_WALK_DISTANCE = 20.0f;
        protected bool  _isMoving;
		protected float _waitTime;

		protected override void SetInitialReferences()
		{
			base.SetInitialReferences();
			_anim = this.GetComponent<Animator>();
			_audio = this.GetComponent<AudioController>();
		}

		protected override void Awake() {
            base.Awake();
            SetStateBehaviour(EAIState.Patrol);
        }
        protected virtual void FixedUpdate() {
            if(_isMoving) {
                if((_navMeshAgent.remainingDistance <= float.Epsilon || _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) {
                    OnPatrolStop();
                }
            }
        }
        protected override void ChangeStateEventHandler(EventData eventData, EAIState state) {
            if(this.BehaviourObservesEvent(eventData) && state == _stateBehaviour) {
                base.ChangeStateEventHandler(eventData, state);
                OnPatrolStart();   
            }
        }
        protected override void EnemyDectedEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData) && _stateBehaviour == _aiStateManagerBehaviour.GetAIState()) {
                OnPatrolStop();
            }
        }
        protected virtual void OnPatrolStart() {
            _isMoving = true;
            Vector3 newPos = RandomNavSphere(transform.position, MAXIMUM_WALK_DISTANCE, -1);
            _navMeshAgent.SetDestination(newPos);
        }
		protected virtual void OnPatrolStop() {
			_isMoving = false;
            if (_aiDetectEnemyBehaviour.GetTarget() == null) {
				_anim.SetBool("Idle", true);
				_anim.SetBool("Patrol", false);
                EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Idle);
            }
            else {
				_anim.SetBool("Patrol", false);
				_anim.SetBool("Chase", true);
				_audio.StartLoop(_audio.ChaseLoop);
				EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Chase);
            }
		}
		public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask) {
			Vector3 randomDirection = Random.insideUnitSphere * distance + origin;
			NavMeshHit navHit;
			NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);
			return navHit.position;
		}
    }
}