using UnityEngine;

namespace Behaviours.BassGuitarBehaviours {
    public class PlayBassGuitarBehaviour : AbstractBehaviour {
		public AudioClip bassNote;
		public AudioClip enemyHit;
		public AudioSource audioSource;
        protected const int MINIMUM_ATTACK_DAMAGE = 0;
        protected const int MAXIMUM_ATTACK_DAMAGE = 100;
        protected const int MINIMUM_ATTACK_DISTANCE = 0;
        protected const int MAXIMUM_ATTACK_DISTANCE = 50;
        protected const float MINIMUM_ATTACK_RADIUS = 0.01f;
        protected const float MAXIMUM_ATTACK_RADIUS = 2.0f;

        protected Camera _visionCamera;
        protected Rigidbody _body;
		public GameObject electricPrefab;
		public Transform electricitySpawnPoint;
		[SerializeField] private float electricBallSpeed = 100.0f;

		[SerializeField]
        protected LayerMask _whatIsAttackable;

        [SerializeField][Range(MINIMUM_ATTACK_DAMAGE, MAXIMUM_ATTACK_DAMAGE)]
        protected float _attackDamage = 5f;

        [SerializeField][Range(MINIMUM_ATTACK_DISTANCE, MAXIMUM_ATTACK_DISTANCE)]
        protected float _attackDistance = 5f;

        [SerializeField][Range(MINIMUM_ATTACK_RADIUS, MAXIMUM_ATTACK_RADIUS)]
        protected float _attackRadius = 0.25f;

        [SerializeField]
        protected float _attackCoolDown = 1.0f;
        protected float _nextAttack = 0f;
        protected bool _playBassButtonPressed = false;

        protected override void SetInitialReferences() {
            base.SetInitialReferences();
            _visionCamera = this.GetComponentInChildren<Camera>();
            _body = this.GetComponent<Rigidbody>();
        }
        protected virtual void Update() {
            _playBassButtonPressed = Input.GetButton("Play Bass Guitar");
        }
        protected virtual void FixedUpdate() {
            if(_playBassButtonPressed) {
                if (Time.time > _nextAttack) {
                    _nextAttack = Time.time + _attackCoolDown;
                    audioSource.PlayOneShot(bassNote);
                    GameObject electricBall = Instantiate(electricPrefab, electricitySpawnPoint.position, _visionCamera.transform.rotation) as GameObject;
					electricBall.GetComponent<Rigidbody>().AddForce(_visionCamera.transform.forward * electricBallSpeed + _body.velocity , ForceMode.Impulse);
                    electricBall.transform.SetParent(null);
					GameObject hitObject = HitScan();
                    OnObjectHit(hitObject);
                    Debug.Log(audioSource.pitch);
                }
            }
        }
        protected virtual GameObject HitScan() {
            Ray ray = Tools.Tools.ScreenPointToRay(_visionCamera);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * _attackDistance, Color.cyan);
            if(Physics.SphereCast(ray, _attackRadius, out hit, _attackDistance, _whatIsAttackable, QueryTriggerInteraction.Ignore)) {
				return hit.collider.gameObject;
            }
            return null;
        }
        protected virtual void OnObjectHit(GameObject hitObject) {
            if (hitObject != null) {
                //Debug.Log(hitObject.name);
                //Debug.Log(audioSource.pitch);
                EventManager.InvokeFrequencyEvent(new EventData(hitObject, this.gameObject), audioSource.pitch, _attackDamage);
            }
        }
        public virtual float GetAttackDamage() {
            return _attackDamage;
        }
        public virtual float GetAttackDistance() {
            return _attackDistance;
        }
        public virtual float GetAttackRadius() {
            return _attackRadius;
        }
    }
}