using System;
using System.Collections.Generic;
using Core.SnakeBoard;
using SnakeGame.Config;
using SnakeGame.Core;
using UnityEngine;

namespace Core.Manager
{
    public class GameManager : IGameManager
    {
        private Board board;
        private Snake snake;

        private int foodPositionOnBoard;

        public ISnake GetSnake => snake;

        public Action<int> UpdateFoodPostion { get; set; }
        public Action IsGameOver { get; set; }
        private bool isGameOver;
        private int totalFoodPlacedTillNow;

        public GameManager (GameConfiguration config)
        {
            board = new Board (config.Width, config.Height);
            snake = new Snake (config.DefaultSnakeLength, config.SnakeMovementSpeed, board);
            totalFoodPlacedTillNow = 0;
        }

        public IEnumerable<Vector2> GetBodyPositions()
        {
            foreach (var body in snake.Body)
            {
                yield return body.GetPos;
            }
        }

        public Vector2 GetSnakeHeadPosition()
        {
            return snake.Head.GetPos;
        }

        public void SetSnakeDirection (Direction direction)
        {
            snake?.SetSnakeDirection (direction);
        }

        public int GetPostionForFoodOnBoard()
        {
            foodPositionOnBoard = board.GetRandomElement().CellPosition;
            return foodPositionOnBoard;
        }

        public void Update (float dt)
        {
            snake?.Update (dt);
            
            if (snake.Head.GetPosOnBoard == foodPositionOnBoard)
            {
                snake.FoodConsumed = true;
                UpdateFoodPostion?.Invoke(++totalFoodPlacedTillNow);
            }

            if (snake.IsBlocked & !isGameOver)
            {
                isGameOver = true;
                IsGameOver?.Invoke();
            }
        }

        public void RestartGame()
        {
            isGameOver = false;
            snake.Reset();
        }
    }
}