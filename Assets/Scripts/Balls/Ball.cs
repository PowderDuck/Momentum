using UnityEngine;

namespace Momentum.Scripts.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        protected Vector2 _direction = Vector2.zero;
        public Vector2 Direction => _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
