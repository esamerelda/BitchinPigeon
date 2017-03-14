using UnityEngine;

namespace Behaviours.AIBehaviours {
    [RequireComponent(typeof(SphereCollider))]
    public class AIDetectEnemyBehaviour : AbstractBehaviour {
        protected const int MINIMUM_RATE = 0;
        protected const int MAXIMUM_RATE = 50;
        protected const int MINIMUM_RADIUS = 0;
        protected const int MAXIMUM_RADIUS = 100;

        protected SphereCollider _detectionSphere;

        [SerializeField]
        protected Transform _target = null;

        [SerializeField]
        protected LayerMask _whatIsEnemy;

        [SerializeField][Range(MINIMUM_RADIUS, MAXIMUM_RADIUS)]
        protected float _detectionRadius = 5f;

        [SerializeField][Range(MINIMUM_RATE, MAXIMUM_RATE)]
        protected float _detectionRate = 5f;

        protected float _nextDetection = 0;

        protected override void OnEnable() {
            base.OnEnable();
            EventManager.DamageEvent += DamageEventHandler;
        }
        protected override void OnDisable() {
            base.OnDisable();
            EventManager.DamageEvent -= DamageEventHandler;
        }
        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _detectionSphere = this.GetComponent<SphereCollider>();
        }
        protected override void SetInitialValues() {
            base.SetInitialValues();
            _detectionSphere.radius = _detectionRadius;
        }
        protected virtual void DamageEventHandler(EventData eventData, float damage) {
            if(this.BehaviourObservesEvent(eventData)) {
                OnDetectEnemy(eventData.GetEventSender().transform);
            }
        }
        protected virtual void OnDetectEnemy(Transform target) {
            _nextDetection = Time.time + _detectionRate;
            SetTarget(target);
            EventManager.InvokeEnemyDetectedEvent(new EventData(this.gameObject, true));
        }
        protected virtual void OnLostEnemy() {
            SetTarget(null);
            EventManager.InvokeEnemyLostEvent(new EventData(this.gameObject, true));
        }
        protected virtual void SetTarget(Transform target) {
            _target = target;
        }
        public virtual Transform GetTarget() {
            return _target;
        }
        protected virtual void OnTriggerEnter(Collider collider) {
            if(Tools.Tools.GetIsLayerInLayerMask(collider.gameObject, _whatIsEnemy)) {
                if (Time.time > _nextDetection) {
                    OnDetectEnemy(collider.transform);
                }
            }
        }
        protected virtual void OnTriggerStay(Collider collider) {
            OnTriggerEnter(collider);
        }
        protected virtual void OnTriggerExit(Collider collider) {
            if(Tools.Tools.GetIsLayerInLayerMask(collider.gameObject, _whatIsEnemy)) {
                OnLostEnemy();
            }
        }
    }
}