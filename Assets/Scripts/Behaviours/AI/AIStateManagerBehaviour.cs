using UnityEngine;

namespace Behaviours.AIBehaviours {
	public class AIStateManagerBehaviour : AbstractBehaviour {
		[SerializeField]
		protected EAIState _state;
        protected override void Awake() {
            base.Awake();
        }
        protected override void Start() {
			base.Start();
            EventManager.InvokeChangeStateEvent(new EventData(this.gameObject, true), EAIState.Idle);
        }
		public void OnChangeState(EAIState state) {
			_state = state;
		}
		public EAIState GetAIState() {
			return _state;
		}
	}
}