using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Bus
{
    public class EventSubscriber : IDisposable
    {
        public Type EventType { get; set; }

        private Action<EventSubscriber> _action;

        public EventSubscriber(Type eventType, Action<EventSubscriber> action)
        {
            EventType = eventType;
            _action = action;
        }

        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }
}
