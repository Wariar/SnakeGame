using System;
using SnakeGame.Application.Manager;
using SnakeGame.Config;
using SnakeGame.Core;

namespace SnakeView
{
    public class ViewPresenter : IViewPresenter
    {
        private IBoardView gameView;
        public ViewPresenter (IBoardView view) 
        {
            this.gameView = view;

            ApplicationManager.SetUpGameBoardView += OnSetUpGameBoardCalled;
            ApplicationManager.FoodPositionUpdated += OnFoodPositionUpdated;
            ApplicationManager.GameOver += OnGameOver;
        }


        private void OnFoodPositionUpdated (int foodPositionOnBoard, int totalPlacedTillNow)
        {
            gameView.PlaceFoodOnBoard (foodPositionOnBoard, totalPlacedTillNow);
        }

        private void OnSetUpGameBoardCalled (GameConfiguration config, ISnake snakeModel)
        {
            DrawBoard 
            (
                config, 
                () =>
                {
                    DrawSnake (config, snakeModel);
                }
            );
        }

        public void DrawBoard (GameConfiguration config, Action successCallback)
        {
            gameView.DrawBoard (config, successCallback);
        }

        private void DrawSnake (GameConfiguration config, ISnake snakeModel)
        {
            gameView.DrawSnake (config, snakeModel);
        }

        private void OnGameOver (bool isGameOver)
        {
            gameView.GameOver(isGameOver);
        }
    }
}