using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Bus
{
    public class MessageSubscriber : IDisposable
    {
        public Type ReceiverType { get; }

        public Type MessageType { get; }

        private readonly Action<MessageSubscriber> action;

        public MessageSubscriber(Type receiverType, Type messageType,Action<MessageSubscriber> action)
        {
            ReceiverType = receiverType;
            MessageType = messageType;
            this.action = action;
        }

        public void Dispose()
        {
            this.action?.Invoke(this);
        }
    }
}
