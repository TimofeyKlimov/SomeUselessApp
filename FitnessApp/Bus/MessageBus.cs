using DevExpress.Mvvm.Native;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Bus
{
    public class MessageBus
    {
        private ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> _subscribers;

        public MessageBus()
        {
            _subscribers = new ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>>();
        }

        public IDisposable Receive<TMessage>(object receiverType,Func<TMessage,Task> func) where TMessage : IMessage
        {
            var subscriber = new MessageSubscriber(receiverType.GetType(),
                typeof(TMessage), s => _subscribers.TryRemove(s, out var _));
            _subscribers.TryAdd(subscriber, s => func((TMessage)s));
            return subscriber;
        }

        public async Task SendTo<TReceiver>(IMessage message)
        {
            var handlers = _subscribers.Where(s => s.Key.MessageType == message.GetType() && s.Key.ReceiverType == typeof(TReceiver))
                .Select(s => s.Value(message));
            await Task.WhenAll(handlers);
        }
    }
}
