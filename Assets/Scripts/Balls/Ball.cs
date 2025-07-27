using UnityEngine;

namespace Momentum.Scripts.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [field: SerializeField]
        public Sprite Sprite { get; private set; }

        protected Vector2 _direction = Vector2.zero;
        public Vector2 Direction => _direction;

        protected Rigidbody2D _rigidbody = default!;

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigidbody.linearVelocity = _direction;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
