using Core.SnakeBoard;
using UnityEngine;

namespace SnakeGame.Core
{
    public class SnakeBody
    {
        public SnakeBody Next { get; set; }
        public Vector2Int GetPos => postion.GetPos;
        public Direction GetDirection => postion.CurrentDirection;
        public int GetPosOnBoard => Utility.GetPosOnBoard (GetPos);
        
        private SnakePosition postion { get; set; }

        public SnakeBody() {}

        public SnakeBody (Vector2Int pos, Direction dir = Direction.NONE)
        {
            postion = new SnakePosition (pos, dir);
        }

        public void UpdatePosition (SnakePosition postion)
        {
            this.postion.Update (postion);
        }

        public void UpdatePosition (Vector2Int nextPosition, Direction nextDirection, Board snakeBoard)
        {
            var previousPosition = GetPos;
            var previousDirection = postion.CurrentDirection;
            
            postion.Update (Utility.GetClampedPosition (nextPosition), nextDirection);
            
            snakeBoard[GetPosOnBoard].IsOccupied = true;

            if (Next != null)
            {
                Next.UpdatePosition (previousPosition, previousDirection, snakeBoard);
            }
        }
    }
}