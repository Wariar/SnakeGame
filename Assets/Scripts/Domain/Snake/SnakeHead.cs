using System.Numerics;
using Core.SnakeBoard;
using UnityEngine;

namespace SnakeGame.Core
{
    public class SnakeHead
    {
        public SnakeBody Next { get; set; }
        private SnakePosition headPosition { get; set; }
        private Vector2Int clampedPostion;
        public Vector2Int GetPos => headPosition.GetPos;
        public int GetPosOnBoard => Utility.GetPosOnBoard (GetPos);

        public bool IsBlocked { get; private set; }

        public SnakeHead (SnakePosition headPosition)
        {
            this.headPosition = new SnakePosition (headPosition);
            IsBlocked = false;
        }

        public void UpdatePosition (SnakePosition headPosition)
        {
            this.headPosition.Update (headPosition);
        }

        public void Move (Direction nextDirection, Board snakeBoard)
        {
            var previousHeadPosition = headPosition.GetPos;
            var previousDirection = headPosition.CurrentDirection;

            var pos = previousHeadPosition;
            pos = pos + Utility.GetDirectionVector (nextDirection);
            pos = Utility.GetClampedPosition (pos);

            if (snakeBoard[Utility.GetPosOnBoard (pos)].IsOccupied) IsBlocked = true;

            headPosition.Update (pos, nextDirection);

            snakeBoard[GetPosOnBoard].IsOccupied = true;

            Next.UpdatePosition (previousHeadPosition, previousDirection, snakeBoard);
        }
    }
}   