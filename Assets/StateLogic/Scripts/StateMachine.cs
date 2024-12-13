using System;
using System.Collections.Generic;
using UnityEngine;
using StateLogic.Events;

namespace StateLogic {
    /// <summary>
    /// Manages states for in-game logic.
    /// </summary>
    /// <remarks>
    /// To use the State Manager, add it in an empty <see cref="GameObject"/> together with any state behaviours
    /// inhering from <see cref="State"/>.
    /// <para />
    /// Inside each <see cref="State"/>, use <see cref="State.TransitionTo(string)"/> to transition between states
    /// in the current state manager.
    /// </remarks>
    [AddComponentMenu("State Machine/State Machine Manager")]
    public sealed class StateMachine : MonoBehaviour {
        private static readonly StateEventBus eventBus;

        [SerializeField]
        private string _managerName;

        [SerializeField]
        private string _initialTransitionEvent;

        private Dictionary<string, State> _states;

        private State _currentState;

        static StateMachine() {
            eventBus = new StateEventBus();
        }

        /// <summary>
        /// Human readable name of this state machine.
        /// </summary>
        public string ManagerName {
            get => _managerName;
            set => _managerName = value;
        }

        /// <summary>
        /// Initial event to be sent when the state machine is started.
        /// </summary>
        /// <remarks>
        /// The key is the state name.
        /// </remarks>
        public string InitialTransitionEvent {
            get => _initialTransitionEvent;
            set => _initialTransitionEvent = value;
        }

        /// <summary>
        /// Name of the current state.
        /// </summary>
        public State CurrentState {
            get => _currentState;
        }

        /// <summary>
        /// States available in this state manager.
        /// </summary>
        /// <remarks>
        /// The key is the state name.
        /// </remarks>
        public Dictionary<string, State> States {
            get => _states;
            set => _states = value;
        }

        /// <summary>
        /// Load available states.
        /// </summary>
        public void LoadStates() {
            _states = new Dictionary<string, State>();
            var states = this.GetComponents<State>();
            foreach (var state in states) {
                state.StateManager = this;
                if (String.IsNullOrEmpty(state.TransitionEvent)) {
                    Debug.LogWarning($"{_managerName} | State {state.StateName} does not have a transition event and will be ignored.");
                    continue;
                }
                _states[state.TransitionEvent] = state;
            }
        }

        /// <summary>
        /// Send an event to the state machine.
        /// </summary>
        /// <remarks>
        /// The event is sent to the underlying <see cref="StateEventBus"/>, which then dispatch the event among all
        /// subscribed state machines.
        /// </remarks>
        /// <param name="eventName">Name of the event being sent.</param>
        public void SendEvent(string eventName) {
            eventBus.SendEvent(eventName);
        }

        private void OnEnable() {
            Debug.Log($"{_managerName} | Subscribe to the Event Bus.");
            eventBus.OnEventSent += OnEventSent;
        }

        private void OnDisable() {
            Debug.Log($"{_managerName} | Unsubscribe from the Event Bus.");
            eventBus.OnEventSent -= OnEventSent;
        }

        private void Awake() {
            LoadStates();
            DisableAllStates();
        }

        private void Start() {
            if (!String.IsNullOrEmpty(_initialTransitionEvent)) {
                this.SendEvent(_initialTransitionEvent);
            }
        }

        private void OnEventSent(StateEventBus source, string eventName) {
            Debug.Log($"{_managerName} | Event sent: {eventName}.");
            this.TransitionStateByEvent(eventName);
        }

        private void DisableAllStates() {
            foreach (var entry in _states) {
                entry.Value.enabled = false;
            }
        }

        private void TransitionStateByEvent(string eventName) {
            var stateToTransition = _states.ContainsKey(eventName) ? _states[eventName] : null;
            if (stateToTransition == null) {
                Debug.Log($"{_managerName} | No transition from event '{eventName}',");
                return;
            }

            if (_currentState == stateToTransition) {
                Debug.LogWarning($"{_managerName} | Trying to transition to current state '{_currentState.StateName}'");
                return;
            }

            Debug.Log($"{_managerName} | Transition from '{_currentState?.StateName}' to '{stateToTransition.StateName}'");

            if (_currentState != null) {
                _currentState.OnExit();
                _currentState.enabled = false;
            }

            _currentState = stateToTransition;
            stateToTransition.enabled = true;
            stateToTransition.OnEnter();
        }
    }
}