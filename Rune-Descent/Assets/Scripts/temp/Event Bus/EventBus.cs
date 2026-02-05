using System;
using System.Collections.Generic;

public static class EventBus {
    private static readonly Dictionary<Type, Delegate> eventTable = new();

    public static void Subscribe<T>(Action<T> listener) {
        if (eventTable.TryGetValue(typeof(T), out var del)) {
            eventTable[typeof(T)] = Delegate.Combine(del, listener);
        }
        else {
            eventTable[typeof(T)] = listener;
        }
    }

    public static void Unsubscribe<T>(Action<T> listener) {
        if (eventTable.TryGetValue(typeof(T), out var del)) {
            var newDel = Delegate.Remove(del, listener);

            if (newDel == null)
                eventTable.Remove(typeof(T));
            else
                eventTable[typeof(T)] = newDel;
        }
    }

    public static void Publish<T>(T publishedEvent) {
        if (eventTable.TryGetValue(typeof(T), out var del)) {
            (del as Action<T>)?.Invoke(publishedEvent);
        }
    }
}
