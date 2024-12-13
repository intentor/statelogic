namespace StateLogic.Events {
    /// <summary>
    /// Represents a dispatch of an event.
    /// </summary>
    /// <param name="source">Source event bus.</param>
    /// <param name="eventName">Dispatched event name.</param>
    public delegate void EventSent(StateEventBus source, string eventName);
}