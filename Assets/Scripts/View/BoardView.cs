using System;
using System.Collections.Generic;
using SnakeGame.Config;
using SnakeGame.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeView
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        public GameObject BoardCellPrefab;
        public GameObject BoardCellsRoot;
        public Canvas UICanvas;
        public Text Score;

        private int width;
        private int height;
        private int foodPlacedPosition;
        private IViewPresenter viewPresenter;
        private List<GridCellView> cells = new List<GridCellView>();
        private ISnake snakeModel;
        private bool canUpdate;
        private Color snakeHeadColor;
        private Color snakeBodyColor;
        private Color foodColorOnBoard;
        private int previousFoodPositionOnBoard;

        public GridCellView this[int pos]
        { 
            get => cells [pos];
            set => cells [pos] = value;
        }

        private void Awake()
        {
            viewPresenter = new ViewPresenter (this);
        }

        public void DrawBoard (GameConfiguration config, Action callback)
        {
            this.width = config.Width;
            this.height = config.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; ++x)
                {
                    GridCellView cell = instantiateCell();
                    cells.Add (cell);
                    setCellPosition(y, x, cell.gameObject);
                    setColor (cell, config.BoardColor);
                }
            }

            callback?.Invoke();

            GridCellView instantiateCell()
            {
                GameObject g = Instantiate(BoardCellPrefab);
                g.transform.SetParent(BoardCellsRoot.transform);
                return g.GetComponent<GridCellView>();
            }

            void setCellPosition (int y, int x, GameObject cell)
            {
                var rectTransform = cell.transform as RectTransform;
                rectTransform.SetAnchorPosX((x * 100) - 450);
                rectTransform.SetAnchorPosY((y * 100) - 450);
                rectTransform.localScale = Vector3.one;
            }

            void setColor (GridCellView g, Color color)
            {
                g.SetCellColor (color);
            }
        }

        public void DrawSnake (GameConfiguration config, ISnake snakeModel)
        {
            this.snakeModel = snakeModel;
            this.snakeHeadColor = config.SnakeHeadColor;
            this.snakeBodyColor = config.SnakeBodyColor;
            this.foodColorOnBoard = config.FoodColor;

            drawHead ();
            drawBody ();

            canUpdate = true;
        }

        private void drawHead ()
        {
            if (snakeModel.Head == null) return;

            Vector2Int headPos = snakeModel.Head.GetPos;
            int boardHeadPos = headPos.y * width + headPos.x;

            this[boardHeadPos].SetCellColor(snakeHeadColor);
        }

        private void drawBody ()
        {
            foreach (var body in snakeModel.Body)
            {
                Vector2Int pos = body.GetPos;
                int bp = pos.y * width + pos.x;
                this[bp].SetCellColor(snakeBodyColor);
            }
        }

        private void Update()
        {
            if (!canUpdate) return;

            drawHead ();
            drawBody ();
            resetPreviousTailNode();
        }

        private void resetPreviousTailNode ()
        {
            if (snakeModel.TailEnd == null) return;

            Vector2Int pos = snakeModel.TailEnd.GetPos;
            int tailPos = pos.y * width + pos.x;
            this[tailPos].ResetColor ();
        }

        public void PlaceFoodOnBoard (int foodPositionOnBoard, int totalPlacedTillNow)
        {
            this[foodPositionOnBoard].SetCellColor (foodColorOnBoard);

            updateScore (totalPlacedTillNow);
        }
        private void updateScore (int totalPlacedTillNow)
        {
            Score.text = totalPlacedTillNow.ToString();
        }

        public void GameOver (bool isGameOver)
        {
            SetGameOver (isGameOver);
        }

        private void SetGameOver (bool isGameOver)
        {
            canUpdate = !isGameOver;
            UICanvas.enabled = isGameOver;

            if (isGameOver)
            {
                foreach (var cell in cells)
                    cell.ResetColor();
            }
        }
    }
}