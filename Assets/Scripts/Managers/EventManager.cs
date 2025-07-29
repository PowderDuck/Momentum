using System;
using Momentum.Scripts.Events;

namespace Momentum.Scripts.Managers
{
    public static class EventManager
    {
        public static event EventHandler<BallsCollidedEventArgs> BallsCollided;

        public static void OnBallsCollided(
            object sender, BallsCollidedEventArgs eventArgs)
        {
            BallsCollided?.Invoke(sender, eventArgs);
        }

        /*private static readonly Dictionary<object, EventHandler<EventArgs>> _callbacks = new();

        public static Action<object, TEventArgs> Register<TEventArgs>(
            object subject, TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            _callbacks.TryAdd(subject, BallsCollided);
            return OnCallback;
        }

        private static void OnCallback(object sender, EventArgs eventArgs)
        {

        }*/
    }
}
