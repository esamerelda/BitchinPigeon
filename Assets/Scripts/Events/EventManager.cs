using UnityEngine;

namespace Behaviours {
    public class EventManager {
        public delegate void Frequency(EventData eventData, float pitch, float damage);
        public static event Frequency FrequencyEvent;

		public delegate void Damage(EventData eventData, float damage);
		public static event Damage DamageEvent;

		public delegate void Heal(EventData eventData, float health);
        public static event Heal HealEvent;

        public delegate void Death(EventData eventData);
        public static event Death DeathEvent;

		public delegate void ChangeState(EventData eventData, EAIState state);
		public static event ChangeState ChangeStateEvent;

        public delegate void EnemyDetected(EventData eventData);
        public static event EnemyDetected EnemyDetectedEvent;

        public delegate void EnemyLost(EventData eventData);
        public static event EnemyLost EnemyLostEvent;

        public delegate void FriendDetected(EventData eventData);
        public static event FriendDetected FriendDetectedEvent;

        public delegate void FriendLost(EventData eventData);
        public static event FriendLost FriendLostEvent;

        public static void InvokeFrequencyEvent(EventData eventData, float pitch, float damage) {
            if(FrequencyEvent != null) {
                FrequencyEvent(eventData, pitch, damage);
            }
        }
        public static void InvokeHealEvent(EventData eventData, float health) {
            if(HealEvent != null) {
                HealEvent(eventData, health);
            }
        }
        public static void InvokeDamageEvent(EventData eventData, float damage) {
            if(DamageEvent != null) {
				Debug.Log("InvokeDamageEvent sender = " + eventData.GetEventSender() + " and observer = " + eventData.GetEventObserver());
                DamageEvent(eventData, damage);
            }
        }
        public static void InvokeDeathEvent(EventData eventData) {
            if(DeathEvent != null) {
                DeathEvent(eventData);
            }
        }
		public static void InvokeChangeStateEvent(EventData eventData, EAIState state) {
			if(ChangeStateEvent != null) {
				ChangeStateEvent(eventData, state);
			}
		}
        public static void InvokeEnemyDetectedEvent(EventData eventData) {
            if(EnemyDetectedEvent != null) {
                EnemyDetectedEvent(eventData);
            }
        }
        public static void InvokeEnemyLostEvent(EventData eventData) {
            if(EnemyLostEvent != null) {
                EnemyLostEvent(eventData);
            }
        }
        public static void InvokeFriendDetectedEvent(EventData eventData) {
            if(FriendDetectedEvent != null) {
                FriendDetectedEvent(eventData);
            }
        }
        public static void InvokeFriendLostEvent(EventData eventData) {
            if(FriendLostEvent != null) {
                FriendLostEvent(eventData);
            }
        }
    }
}