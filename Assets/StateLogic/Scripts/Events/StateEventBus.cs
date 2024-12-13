namespace StateLogic.Events {
    /// <summary>
    /// Handles event dispatching among state machines.
    /// </summary>
    public class StateEventBus {
        /// <summary>
        /// Called when a event is sent through the event bus.
        /// </summary>
        public event EventSent OnEventSent;

        /// <summary>
        /// Send an event to all subscribers.
        /// </summary>
        /// <param name="eventName">Name of the event being sent.</param>
        public void SendEvent(string eventName) {
            OnEventSent?.Invoke(this, eventName);
        }
    }
}