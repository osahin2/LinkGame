using System;
using System.Collections.Generic;

namespace EventBusSystem
{
    public class EventBus<TEventArgs>
        where TEventArgs : EventArgs
    {
        private static readonly Dictionary<string, Action<TEventArgs>> _eventMap = new();

        public static void RegisterEvent(string eventType, Action<TEventArgs> eventHandler)
        {
            if (!_eventMap.ContainsKey(eventType))
            {
                _eventMap[eventType] = eventHandler;
            }
            else
            {
                _eventMap[eventType] += eventHandler;
            }
        }

        public static void DeRegisterEvent(string eventType, Action<TEventArgs> eventHandler)
        {
            if (_eventMap.ContainsKey(eventType))
            {
                _eventMap[eventType] -= eventHandler;
            }
        }

        public static void TriggerEvent(string eventType, TEventArgs eventArgs)
        {
            if (_eventMap.ContainsKey(eventType))
            {
                _eventMap[eventType]?.Invoke(eventArgs);
            }
        }
    }
}
