namespace Core.SnakeBoard
{
    public enum Direction
    {
        NONE = 0,
        UP = 2,
        DOWN = 4,
        LEFT = 8,
        RIGHT = 16,

        OPPOSITE_V = UP | DOWN,
        OPPOSITE_H = RIGHT | LEFT
    }
}