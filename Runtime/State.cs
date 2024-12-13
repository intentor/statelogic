using System;
using UnityEngine;

namespace StateLogic {
    /// <summary>
    /// In-game state component.
    /// </summary>
    [RequireComponent(typeof(StateMachine))]
    public abstract class State : MonoBehaviour {
        [SerializeField]
        [HideInInspector]
        private string _stateName;

        [SerializeField]
        [HideInInspector]
        private string _transitionEvent;

        /// <summary>
        /// State manager main instance.
        /// </summary>
        public StateMachine StateManager { get; set; }

        /// <summary>
        /// Human readable name of this state.
        /// </summary>
        public string StateName {
            get => _stateName;
            set => _stateName = value;
        }

        /// <summary>
        /// Name of the event which transitions to this state.
        /// </summary>
        public string TransitionEvent {
            get => _transitionEvent;
            set => _transitionEvent = value;
        }

        /// <summary>
        /// Called when entering the state.
        /// </summary>
        public virtual void OnEnter() {
            // Does nothing.
        }

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public virtual void OnExit() {
            // Does nothing.
        }

        /// <summary>
        /// Send an event to the parent state machine.
        /// </summary>
        /// <param name="eventName">Name of the event being sent.</param>
        protected void SendEvent(string eventName) {
            this.StateManager.SendEvent(eventName);
        }
    }
}