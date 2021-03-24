using Core.SnakeBoard;
using UnityEngine;

namespace SnakeGame.Core
{
    public class SnakePosition
    {
        public int XPos { get; private set; }
        public int YPos { get; private set; }

        public Direction CurrentDirection { get; private set; }

        public Vector2Int GetPos => Vector2Int.right * XPos + Vector2Int.up * YPos;

        public SnakePosition (SnakePosition initialPostiion)
        {
            XPos = initialPostiion.GetPos.x;
            YPos = initialPostiion.GetPos.y;
        }

        public SnakePosition (Vector2Int pos, Direction dir)
        {
            Update (pos, dir);
        }

        public void Update (Vector2Int pos, Direction dir)
        {
            XPos = pos.x;
            YPos = pos.y;

            CurrentDirection = dir;
        }

        public void Update (SnakePosition pos)
        {
            XPos = pos.GetPos.x;
            YPos = pos.GetPos.y;

            CurrentDirection = pos.CurrentDirection;
        }
    }
}