using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Momentum.Scripts.Balls
{
    public class CommonBall : Ball
    {
        [SerializeField] private List<SpriteRenderer> _ballSides = default!;

        private readonly List<Ball> _balls = new();
        private int _currentBallIndex = 0;

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Ball>(out var colliderBall))
            {
                _balls.Add(colliderBall);

                _ballSides[_currentBallIndex].sprite =
                    collider.GetComponent<SpriteRenderer>().sprite;
                _currentBallIndex = (_currentBallIndex + 1) % _balls.Count;

                if (_balls.Count >= _ballSides.Count
                    && _balls.All(ball => ball.GetType() == colliderBall.GetType()))
                {
                    var releasedBall = Instantiate(
                        _balls[0],
                        transform.position,
                        Quaternion.identity);
                    releasedBall.SetDirection(colliderBall.Direction);

                    _balls.Clear();
                    foreach (var spriteRenderer in _ballSides)
                    {
                        spriteRenderer.sprite = null;
                    }
                }
            }
        }
    }
}
