﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TurnBasedStrategyToy
{
    public class BattleUnit : MonoBehaviour
    {
        public Vector3 WorldPosition => _self.WorldPosition;

        [SerializeField]
        private BattleUnitStats _stats;
        [SerializeField]
        private ObjectOnGrid _self;
        private List<Vector2Int> _reachableTiles = new List<Vector2Int>();

        public void AnimateTo(Vector2Int position)
        {
            position = ClampInMovement(position);
            _self.AnimateTo(position);
        }

        public Vector2Int ClampInMovement(Vector2Int position)
        {
            MovementReachableTiles(_reachableTiles);
            if (_reachableTiles.Count == 0)
            {
                return position;
            }
            int shortestIndex = 0;
            int shortestDistanceX = int.MaxValue;
            int shortestDistanceY = int.MaxValue;
            for (int i = 0; i < _reachableTiles.Count; i++)
            {
                Vector2Int reachableTile = _reachableTiles[i];
                int currentDistanceX = Mathf.Abs(position.x - reachableTile.x);
                int currentDistanceY = Mathf.Abs(position.y - reachableTile.y);
                if (currentDistanceX < shortestDistanceX || currentDistanceY < shortestDistanceY)
                {
                    shortestIndex = i;
                    shortestDistanceX = currentDistanceX;
                    shortestDistanceY = currentDistanceY;
                }
            }
            return _reachableTiles[shortestIndex];
        }

        private void MovementReachableTiles(List<Vector2Int> tiles)
        {
            Assert.IsNotNull(tiles);

            tiles.Clear();
            for (int x = -_stats.Movement; x <= _stats.Movement; x++)
            {
                for (int y = -_stats.Movement; y <= _stats.Movement; y++)
                {
                    Vector2Int delta = new Vector2Int(x, y);
                    if (AbsoluteSum(delta) > _stats.Movement)
                    {
                        continue;
                    }
                    Vector2Int position = _self.GridPosition + delta;
                    tiles.Add(position);
                }
            }
        }
        private int AbsoluteSum(Vector2Int vector)
        {
            return Mathf.Abs(vector.x) + Mathf.Abs(vector.y);
        }

        private void OnDrawGizmosSelected()
        {
            MovementReachableTiles(_reachableTiles);
            foreach (Vector2Int position in _reachableTiles)
            {
                Gizmos.DrawWireCube(_self.GridToWorld(position), Vector3.one);
            }
        }
    }
}