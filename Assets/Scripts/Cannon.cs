using System.Collections.Generic;
using Momentum.Scripts.Balls;
using UnityEngine;

namespace Momentum.Scripts
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spoilerRenderer = default!;

        [SerializeField] private Transform _upperBoundary;
        [SerializeField] private Transform _lowerBoundary;

        [SerializeField] private List<Ball> _ballPrefabs = default!;

        private Vector2 _cannonPosition = Vector2.zero;

        private Ball _currentBall = default!;

        private void Start()
        {
            _cannonPosition = transform.position;

            NextBall(_ballPrefabs[Random.Range(0, _ballPrefabs.Count)]);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ball = Instantiate(
                    _currentBall,
                    transform.position,
                    Quaternion.identity);
                ball.SetDirection(transform.right);
                NextBall(_ballPrefabs[Random.Range(0, _ballPrefabs.Count)]);
            }
        }

        private void FixedUpdate()
        {
            _cannonPosition.Set(
                _cannonPosition.x,
                Mathf.Clamp(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    _lowerBoundary.position.y,
                    _upperBoundary.position.y));

            transform.position = _cannonPosition;
        }

        private void NextBall(Ball ball)
        {
            _currentBall = ball;
            _spoilerRenderer.sprite = ball.Sprite;
        }
    }
}
