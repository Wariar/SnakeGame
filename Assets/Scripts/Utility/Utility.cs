using System.Collections;
using System.Collections.Generic;
using Core.SnakeBoard;
using UnityEngine;

public static class Utility
{
    private static Dictionary<Direction, Vector2Int> directionToVectorMapping = new Dictionary<Direction, Vector2Int>()
                                                                        {
                                                                            { Direction.UP, Vector2Int.up },
                                                                            { Direction.DOWN, Vector2Int.down },
                                                                            { Direction.RIGHT, Vector2Int.right },
                                                                            { Direction.LEFT, Vector2Int.left }
                                                                        };

    public static Vector2Int GetDirectionVector (Direction direction)
    {
        if (directionToVectorMapping.ContainsKey (direction))
            return directionToVectorMapping [direction];
        
        return Vector2Int.zero;
    }

    public static Vector2Int GetClampedPosition (Vector2Int pos)
    {
        pos.x = (pos.x + Board.Width) % Board.Width;
        pos.y = (pos.y + Board.Height) % Board.Height;

        return pos;
    }

    public static int GetPosOnBoard (Vector2Int pos)
    {
        return (pos.y * Board.Width) + pos.x;
    }

    public static void SetSizeDeltaX (this RectTransform rectTransform, float x)
    {
        Vector2 sd = rectTransform.sizeDelta;
        sd.x = x;
        rectTransform.sizeDelta = sd;
    }

    public static void SetSizeDeltaY (this RectTransform rectTransform, float y)
    {
        Vector2 sd = rectTransform.sizeDelta;
        sd.y = y;
        rectTransform.sizeDelta = sd;
    }

    public static void SetAnchorPosX (this RectTransform rectTransform, float x)
    {
        Vector2 sd = rectTransform.anchoredPosition;
        sd.x = x;
        rectTransform.anchoredPosition = sd;
    }

    public static void SetAnchorPosY (this RectTransform rectTransform, float y)
    {
        Vector2 sd = rectTransform.anchoredPosition;
        sd.y = y;
        rectTransform.anchoredPosition = sd;
    }

    public static void SetRightDelta (this RectTransform rectTransform, float delta)
    {
        Vector2 sd = rectTransform.offsetMax;
        sd.x = -delta;
        rectTransform.offsetMax = sd;
    }

    public static void SetLeftDelta(this RectTransform rectTransform, float delta)
    {
        Vector2 sd = rectTransform.offsetMin;
        sd.x = delta;
        rectTransform.offsetMin = sd;
    }

    public static void SetTopDelta (this RectTransform rectTransform, float delta)
    {
        Vector2 sd = rectTransform.offsetMax;
        sd.y = -delta;
        rectTransform.offsetMax = sd;
    }

    public static void SetBottomDelta (this RectTransform rectTransform, float delta)
    {
        Vector2 sd = rectTransform.offsetMin;
        sd.y = delta;
        rectTransform.offsetMin = sd;
    }

    public static T GetRandomCell<T> (this IList<T> list)
    {
        return list [Random.Range (0, list.Count)];
    }
}