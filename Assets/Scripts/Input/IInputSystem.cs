using System;
using Core.SnakeBoard;

namespace SnakeGame.GameInput
{
    public interface IInputSystem
    {
        void Update (float dt);

        Action<Direction> OnDirectionChanged { get; set; }
    }
}