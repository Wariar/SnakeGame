namespace Core.SnakeBoard
{
    public class BoardCell
    {
        public int CellPosition { get; private set; }
        public bool IsOccupied { get; set; }

        public BoardCell (int position)
        {
            CellPosition = position;
            IsOccupied = false;
        }
    }
}