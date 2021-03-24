using UnityEngine;

namespace SnakeGame.Config
{    
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "SnakeGame/GameConfiguration", order = 0)]
    public class GameConfiguration : ScriptableObject
    {
        [Header ("Grid Size")]
        public int Width;
        public int Height;
        
        [Header ("Snake Properties")]
        public int DefaultSnakeLength;
        public float SnakeMovementSpeed;

        [Header ("Board Properties")]
        public Color SnakeBodyColor;
        public Color SnakeHeadColor;
        public Color BoardColor;
        public Color FoodColor;

    }
}