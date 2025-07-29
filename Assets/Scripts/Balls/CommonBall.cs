using System.Collections.Generic;
using UnityEngine;

namespace Momentum.Scripts.Balls
{
    public class CommonBall : Ball
    {
        private const int RequiredMatches = 2;

        private readonly List<Ball> _balls = new();

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Ball>(out var ball))
            {
                _balls.Add(ball);
            }
        }

        private void FillBall(Ball ball, bool upper)
        {

        }
    }
}
