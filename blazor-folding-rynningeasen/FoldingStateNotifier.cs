using System;
using System.Collections.Generic;

namespace blazor_folding_rynningeasen
{
    public class FoldingStateNotifier : INotifier
    {
        private static readonly Dictionary<Guid, Action<KeyValuePair<string, string>>> Subscribers =
            new Dictionary<Guid, Action<KeyValuePair<string, string>>>();

        public Guid Register(Action<KeyValuePair<string, string>> callback)
        {
            var id = Guid.NewGuid();
            Subscribers.Add(id, callback);

            return id;
        }

        public void Push(KeyValuePair<string, string> data)
        {
            CurrentState[data.Key] = data.Value;

            foreach (var subscriber in Subscribers)
            {
                subscriber.Value(data);
            }
        }

        public void Unregister(Guid subscriberId)
        {
            Subscribers.Remove(subscriberId);
        }

        public Dictionary<string, string> CurrentState { get; private set; } = new Dictionary<string, string>();
    }
}
