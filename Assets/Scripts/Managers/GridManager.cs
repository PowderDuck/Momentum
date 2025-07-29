using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Momentum.Scripts.Balls;
using Momentum.Scripts.Events;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Momentum.Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private List<Ball> _ballPrefabs = default!;

        [SerializeField] private Vector2 _gridSize = new(4, 7);
        [SerializeField] private Vector2 _offset = new(1, 1);
        [SerializeField] private Transform _gridCenter = default!;

        private readonly Dictionary<Vector2, Ball> _grid = new();

        private void Start()
        {
            for (var x = 0; x < _gridSize.x; x++)
            {
                for (var y = 0; y < _gridSize.y; y++)
                {
                    var gridPosition = new Vector2(x, y);
                    var ball = Instantiate(
                        _ballPrefabs[Random.Range(0, _ballPrefabs.Count)],
                        GridToWorld(gridPosition),
                        Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));
                    ball.transform.SetParent(_gridCenter, false);

                    _grid.Add(gridPosition, ball);
                }
            }

            EventManager.BallsCollided += OnBallsCollided;
        }

        private void OnBallsCollided(
            object sender, BallsCollidedEventArgs eventArgs)
        {
            var firstBallMagnitude = eventArgs.First.Direction.magnitude;
            var secondBallMagnitude = eventArgs.Second.Direction.magnitude;
            if (firstBallMagnitude > 0 && secondBallMagnitude > 0)
            {
                Debug.Log("Two Moving Balls Collided !");
                return;
            }

            // Item1 = staticBall, Item2 = dynamicBall
            var balls = firstBallMagnitude > 0
                ? new Tuple<Ball, Ball>(eventArgs.Second, eventArgs.First)
                : new Tuple<Ball, Ball>(eventArgs.First, eventArgs.Second);

            if (!_grid.ContainsValue(balls.Item1))
            {
                Debug.LogWarning("Ball is not in a grid");
                return;
            }

            var direction = balls.Item2.Direction;
            var gridPosition = _grid.FirstOrDefault(
                pair => pair.Value == balls.Item1).Key;

            var currentBall = balls.Item2;
            while (currentBall != null)
            {
                _grid[gridPosition] = currentBall;
                currentBall.transform
                    .DOMove(GridToWorld(gridPosition), 2)
                    .SetEase(Ease.Linear);

                gridPosition.Set(
                    Mathf.Floor(gridPosition.x + direction.normalized.x),
                    Mathf.Floor(gridPosition.y + direction.normalized.y));
                if (!_grid.TryGetValue(gridPosition, out var nextBall))
                {
                    break;
                }

                currentBall = nextBall;
            }

            currentBall.SetDirection(direction);
        }

        private Vector2 GridToWorld(Vector2 gridPosition)
        {
            return (Vector2)_gridCenter.transform.position
                + new Vector2(
                    (gridPosition.x - ((_gridSize.x - 1f) / 2f)) * _offset.x,
                    (gridPosition.y - ((_gridSize.y - 1f) / 2f)) * _offset.y);
        }

        private void OnDestroy()
        {
            EventManager.BallsCollided -= OnBallsCollided;
        }
    }
}
