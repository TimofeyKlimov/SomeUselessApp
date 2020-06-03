using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Bus
{
    public class EventBus
    {
        private ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>> _subscribers;

        public EventBus()
        {
            _subscribers = new ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>>();
        }

        public IDisposable Send<TEvent>(Func<TEvent,Task> func) where TEvent:IEvent
        {
            var eventSubscriber = new EventSubscriber(typeof(TEvent), s => _subscribers.TryRemove(s, out var _));
            _subscribers.TryAdd(eventSubscriber, s => func((TEvent)s));
            return eventSubscriber;
        }

        public Task Receive(IEvent eventSub)
        {
            var eventType = typeof(IEvent);
            var handlers = _subscribers.Where(s => s.Key.EventType == eventType)
                .Select(s => s.Value(eventSub));
            return Task.WhenAll(handlers);
        }
    }
}
