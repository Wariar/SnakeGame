using System;
using System.Collections.Generic;
using Core.SnakeBoard;
using UnityEngine;

namespace SnakeGame.GameInput
{
    public class InputSystem : IInputSystem
    {
        public Action<Direction> OnDirectionChanged { get; set; }

        public void Update (float dt)
        {
            if (IsRequiredKeyPressed)
            {
                OnDirectionChanged?.Invoke ( GetDirection );
            }
        }

        private Direction GetDirection
        {
            get 
            {
                Direction dir = Direction.NONE;

                dir = Input.GetKeyUp (KeyCode.UpArrow) ? Direction.UP
                        : Input.GetKeyUp (KeyCode.DownArrow) ? Direction.DOWN
                        : Input.GetKeyUp (KeyCode.LeftArrow) ? Direction.LEFT
                        : Input.GetKeyUp (KeyCode.RightArrow) ? Direction.RIGHT
                        : Direction.NONE;

                return dir;
            }
        }

        public bool IsRequiredKeyPressed => Input.GetKeyUp (KeyCode.UpArrow)
                                            | Input.GetKeyUp (KeyCode.DownArrow)
                                            | Input.GetKeyUp (KeyCode.RightArrow)
                                            | Input.GetKeyUp (KeyCode.LeftArrow);

    }
}