using SnakeGame.GameSave;
using UnityEngine;
using SnakeGame.GameInput;
using Core.SnakeBoard;
using System;
using SnakeGame.Config;
using SnakeGame.Core;
using Core.Manager;

namespace SnakeGame.Application.Manager
{
    public class ApplicationManager : MonoBehaviour
    {
        public IGameManager SnakeManager { get; set; }
        public IInputSystem SnakeInput { get; set; }
        private GameConfiguration gameConfiguration;
        public IGameSave SnakeSave { get; set; }

        public static Action<GameConfiguration, ISnake> SetUpGameBoardView;
        public static Action<int, int> FoodPositionUpdated;
        public static Action<bool> GameOver;

        private float resetTimer = 2.0f;
        private float resetdelta;
        private bool isGameOver;

        private void Awake()
        {
            gameConfiguration = Resources.Load<GameConfiguration> ("GameConfiguration");
            SnakeManager = new GameManager (gameConfiguration);
            SnakeInput = new InputSystem ();

            SnakeManager.UpdateFoodPostion += OnFoodPositionUpdated;
            SnakeManager.IsGameOver += IsGameOver;
        }

        private void Start()
        {
            Screen.SetResolution (1080, 1920, true);
            
            SnakeInput.OnDirectionChanged += OnInputDirectionChanged;

            SetUpGameBoardView?.Invoke (gameConfiguration, SnakeManager.GetSnake);
            OnFoodPositionUpdated(0);
        }

        private void IsGameOver()
        {
            isGameOver = true;
            GameOver?.Invoke(isGameOver);
        }

        private void OnFoodPositionUpdated (int totalPlacedTillNow)
        {
            FoodPositionUpdated?.Invoke (SnakeManager.GetPostionForFoodOnBoard(), totalPlacedTillNow);
        }

        private void OnInputDirectionChanged (Direction direction)
        {
            SnakeManager?.SetSnakeDirection (direction);
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            
            SnakeManager?.Update (dt);
            SnakeInput?.Update (dt);
            SnakeSave?.Update (dt);

            if (isGameOver)
            {
                resetdelta += dt;
                if (resetdelta > resetTimer)
                {
                    resetdelta = 0;
                    isGameOver = false;
                    GameOver?.Invoke(isGameOver);
                    SnakeManager.RestartGame();
                    OnFoodPositionUpdated(0);
                }
            }
        }
    }
}