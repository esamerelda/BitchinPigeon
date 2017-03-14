using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.AIBehaviours {
    [RequireComponent(typeof(AIStateManagerBehaviour))]
    public abstract class AbstractAIStateBehaviour : AbstractBehaviour {
        protected AIStateManagerBehaviour   _aiStateManagerBehaviour;
        protected AIDetectEnemyBehaviour    _aiDetectEnemyBehaviour;
        protected NavMeshAgent              _navMeshAgent;

        protected EAIState _stateBehaviour;

        protected override void OnEnable() {
            base.OnEnable();
            EventManager.ChangeStateEvent += ChangeStateEventHandler;
            EventManager.EnemyDetectedEvent += EnemyDectedEventHandler;
            EventManager.DeathEvent += DeathEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.ChangeStateEvent -= ChangeStateEventHandler;
            EventManager.EnemyDetectedEvent -= EnemyDectedEventHandler;
            EventManager.DeathEvent -= DeathEventHandler;
        }
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _aiStateManagerBehaviour    = this.GetComponent<AIStateManagerBehaviour>();
            _aiDetectEnemyBehaviour     = this.GetComponent<AIDetectEnemyBehaviour>();
            _navMeshAgent               = this.GetComponent<NavMeshAgent>();
        }
        protected virtual void SetStateBehaviour(EAIState state) {
            _stateBehaviour = state;
        }
        protected virtual void ChangeStateEventHandler(EventData eventData, EAIState state) {
            _aiStateManagerBehaviour.OnChangeState(state);
        }
        protected virtual void EnemyDectedEventHandler(EventData eventData) {
            //Implement in child classes.
        }
        protected virtual void FriendDetectedEventHandler(EventData eventData) {
            //Implement in child classes
        }
        protected virtual void DeathEventHandler(EventData eventData) {
            if(this.BehaviourObservesEvent(eventData)) {
                _navMeshAgent.enabled = false;
                this.OnDisableBehaviour();
            }
        }
    }
}