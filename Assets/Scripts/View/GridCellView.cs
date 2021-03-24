using System;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeView
{
    public class GridCellView : MonoBehaviour
    {
        [SerializeField] private Image cellImage;

        public Color PreviousColor { get; private set; } = default (Color);
        
        public void SetCellColor (Color color)
        {
            if (PreviousColor == default (Color))
                PreviousColor = color;
            
            cellImage.color = color;
        }

        public void ResetColor()
        {
            cellImage.color = PreviousColor;
        }
    }
}