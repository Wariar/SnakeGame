using System;
using System.Collections.Generic;

namespace Core.SnakeBoard
{
    public class Board
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        private IList<BoardCell> gridList;

        public int Count => gridList.Count - 1;

        public Board (int width, int height)
        {
            Width = width;
            Height = height;

            gridList = new List<BoardCell> (Width * Height);

            Generate();
        }

        public BoardCell this [int index]
        {
            get => gridList [index];

            set => gridList [index] = value;
        }

        private void Generate()
        {
            int size = Width * Height;

            for (int index = 0; index < size; ++index)
            {
                gridList.Add (new BoardCell (index));
            }
        }

        public BoardCell GetRandomElement()
        {
            return gridList.GetRandomCell();
        }

        public void Reset ()
        {
            foreach (var cell in gridList)
                cell.IsOccupied = false;
        }
    }
}