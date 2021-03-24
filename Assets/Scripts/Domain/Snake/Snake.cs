using UnityEngine;
using System.Collections.Generic;
using Core.SnakeBoard;
using SnakeGame.Config;
using System.Numerics;
using System;

namespace SnakeGame.Core
{
    public class Snake : ISnake
    {
        public SnakeHead Head { get; private set; }
        public IList<SnakeBody> Body { get; private set; }
        public SnakeBody TailEnd { get; private set; }
        public bool FoodConsumed {get; set;}
        public bool IsBlocked => Head.IsBlocked;

        private Board snakeBoard;
        private Queue<Direction> nextDirections = new Queue<Direction> ();
        private float stepTimer;
        private float maxStepTimer = 1;
        private readonly int initialSnakeLength;
        private readonly float snakeMovementSpeed;

        public Snake (int initialSnakeLength, float speed, Board board)
        {
            this.initialSnakeLength = initialSnakeLength;
            this.snakeMovementSpeed = speed;
            snakeBoard = board;
            Body = new List<SnakeBody>();
            Init();
        }

        private void Init ()
        {
            SnakePosition hPos = initHead();
            initBodyAndTail (hPos);

            SnakePosition initHead ()
            {
               var cell = snakeBoard.GetRandomElement();

                int xPos = cell.CellPosition % Board.Width;
                int yPos = cell.CellPosition / Board.Width;

                var headPosition = new SnakePosition (new Vector2Int (xPos, yPos), Direction.RIGHT);
                Head = new SnakeHead (headPosition);

                cell.IsOccupied = true;

                return headPosition;
            }

            void initBodyAndTail(SnakePosition headPosition)
            {
                int index = 0;
                for (; index < initialSnakeLength; ++index)
                {
                    var pos = headPosition.GetPos + Vector2Int.left * (index + 1);
                    Body.Add(new SnakeBody (Utility.GetClampedPosition (pos)));

                    snakeBoard[Body[index].GetPosOnBoard].IsOccupied = true;

                    if (index > 0)
                        Body[index - 1].Next = Body[index];
                }

                Head.Next = Body[0];

                var tp = headPosition.GetPos + Vector2Int.left * (index + 1);
                TailEnd = new SnakeBody(Utility.GetClampedPosition (tp));
                Body[index - 1].Next = TailEnd;

                snakeBoard[TailEnd.GetPosOnBoard].IsOccupied = true;
            }
        }

        public void SetSnakeDirection (Direction direction)
        {
            if (!IsAFlipDirection (direction))
            {
                nextDirections.Enqueue (direction);
            }
        }

        public void Update (float dt)
        {
            if (Head.IsBlocked) return;

            if (nextDirections == null || nextDirections.Count == 0 || nextDirections.Peek() == Direction.NONE) return;

            stepTimer += dt * snakeMovementSpeed;
            if (stepTimer > maxStepTimer)
            {
                if (FoodConsumed)
                {
                    FoodConsumed = false;
                    getLonger();
                }

                stepTimer = 0;

                var nextDir = nextDirections.Count > 1 ? nextDirections.Dequeue() : nextDirections.Peek();
                Head.Move (nextDir, snakeBoard);
            }

            snakeBoard[TailEnd.GetPosOnBoard].IsOccupied = false;

            void getLonger()
            {
                var newBody = new SnakeBody(TailEnd.GetPos, TailEnd.GetDirection);
                TailEnd.UpdatePosition(TailEnd.GetPos + Utility.GetDirectionVector(TailEnd.GetDirection),
                                        TailEnd.GetDirection,
                                        snakeBoard);
                newBody.Next = TailEnd;
                Body[Body.Count - 1].Next = newBody;
                Body.Add(newBody);
            }
        }

        private bool IsAFlipDirection (Direction direction)
        {
            if (direction == Direction.LEFT && (nextDirections == null || nextDirections.Count == 0)) return true;
            if (nextDirections == null || nextDirections.Count == 0) return false;

            return ( (nextDirections.Peek() | direction) == Direction.OPPOSITE_V ) ||
                    ( (nextDirections.Peek() | direction) == Direction.OPPOSITE_H );
        }

        public void Reset()
        {
            nextDirections.Clear();
            snakeBoard.Reset();
            Head = null;
            Body.Clear();
            TailEnd = null;
            stepTimer = 0;
            Init();
        }
    }
}