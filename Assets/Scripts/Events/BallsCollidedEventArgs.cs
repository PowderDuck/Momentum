using System;
using Momentum.Scripts.Balls;

namespace Momentum.Scripts.Events
{
    public class BallsCollidedEventArgs : EventArgs
    {
        public Ball First { get; }
        public Ball Second { get; }

        public BallsCollidedEventArgs(Ball first, Ball second)
        {
            First = first;
            Second = second;
        }
    }
}