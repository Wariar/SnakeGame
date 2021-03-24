using System;
using System.Collections;
using System.Collections.Generic;
using Core.SnakeBoard;
using SnakeGame.Core;
using UnityEngine;

namespace Core.Manager
{
    public interface IGameManager
    {
        int GetPostionForFoodOnBoard();
        void Update (float dt);
        void SetSnakeDirection (Direction direction);
        Vector2 GetSnakeHeadPosition();
        IEnumerable<Vector2> GetBodyPositions ();
        ISnake GetSnake {get;}
        Action<int> UpdateFoodPostion { get; set; }
        Action IsGameOver { get; set; }
        void RestartGame();
    }
}