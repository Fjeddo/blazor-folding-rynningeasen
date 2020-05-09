using System;
using System.Collections.Generic;

namespace blazor_folding_rynningeasen
{
    public interface INotifier
    {
        Guid Register(Action<KeyValuePair<string, string>> callback);

        void Push(KeyValuePair<string, string> data);
        void Unregister(Guid subscriberId);

        Dictionary<string, string> CurrentState { get; }
    }
}