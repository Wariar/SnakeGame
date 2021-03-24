using System.Collections.Generic;

namespace SnakeGame.Core
{
    public interface ISnake
    {
        SnakeHead Head { get;}
        IList<SnakeBody> Body { get;}
        SnakeBody TailEnd {get;}
    }
}