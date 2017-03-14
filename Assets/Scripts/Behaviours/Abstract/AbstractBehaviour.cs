using UnityEngine;

namespace Behaviours {
    public abstract class AbstractBehaviour : MonoBehaviour {
        /**
         * OnEnable is called whenever the Behaviour is enabled after being disabled. 
         * This method is called after the Awake method.
         */ 
        protected virtual void OnEnable() {
            //Implement in child class.
        }
        /**
         * OnDisable is called whenever the Behaviour is disabled after being enabled.
         */
        protected virtual void OnDisable() {
            //Implement in child class.
        }
        /**
         * Awake is called on first script execution, it is the first method to be called.
         */ 
        protected virtual void Awake() {
            //Set initial references for components.
            this.SetInitialReferences();
        }
        /**
         * Start is called after Awake and OnEnable, the method is called only once.
         */ 
        protected virtual void Start() {
            //Set initial values for data memebers.
            this.SetInitialValues();
        }
        /**
         * SetInitialReferences sets any and all initial references for the Beahviour's components
         */ 
        protected virtual void SetInitialReferences() {
            //Implement in child classes.
        }
        /**
         * SetInitialValues sets any and all initial values needed for the Behaviour's data membebers.
         */
        protected virtual void SetInitialValues() {
            //Implement in child classes.
        }
        /**
         * BehaviourObservesEvent returns true if the Behaviour observers an invoked event.
         */ 
        protected virtual bool BehaviourObservesEvent(EventData eventData) {
            return (eventData.GetIsLocal() && eventData.GetEventObserver() == this.gameObject) || !eventData.GetIsLocal();
        }
        /**
         * OnEnableBehaviour enables the Behaviour.
         */
        public virtual void OnEnableBehaviour() {
            //Enable the Behaviour.
            this.enabled = true;
        }
        /**
         * OnDisableBehavior disables the Behaviour.
         */ 
        public virtual void OnDisableBehaviour() {
            //Disable the Behaviour.
            this.enabled = false;
        }
        public virtual void SetParent(Transform parent) {
            this.transform.SetParent(parent);
        }
        public virtual void SetParent(Transform parent, bool worldPositionStays) {
            this.transform.SetParent(parent, worldPositionStays);
        }
        public virtual void SetPosition(Vector3 position) {
            this.transform.position = position;
        }
        public virtual void SetLocalPosition(Vector3 position) {
            this.transform.localPosition = position;
        }
        public virtual void SetRotation(Vector3 rotation) {
            this.transform.rotation = Quaternion.Euler(rotation);
        }
        public virtual void SetLocalRotation(Vector3 rotation) {
            this.transform.localRotation = Quaternion.Euler(rotation);
        }
        public virtual void SetScale(Vector3 scale) {
            this.transform.localScale = scale;
        }
        public virtual string GetName() {
            return this.gameObject.name;
        }
        public virtual Transform GetParentTransform() {
            return this.transform.parent;
        }
        public virtual GameObject GetParentGameObject() {
            return this.transform.parent.gameObject;
        }
        public virtual Vector3 GetPosition() {
            return this.transform.position;
        }
        public virtual Vector3 GetLocalPosition() {
            return this.transform.localPosition;
        }
        public virtual Quaternion GetRotation() {
            return this.transform.rotation;
        }
        public virtual Quaternion GetLocalRotation() {
            return this.transform.localRotation;
        }
        public virtual Vector3 GetScale() {
            return this.transform.localScale;
        }
    }
}