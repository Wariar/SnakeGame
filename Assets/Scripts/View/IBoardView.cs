using System.Collections.Generic;
using SnakeGame.Config;

namespace SnakeView
{
    public interface IBoardView
    {
        void DrawBoard (GameConfiguration config, System.Action successCallback);
        void DrawSnake (GameConfiguration config, SnakeGame.Core.ISnake snakeModel);
        void PlaceFoodOnBoard (int foodPositionOnBoard, int totalPlacedTillNow);
        void GameOver (bool isGameOver);
    }
}