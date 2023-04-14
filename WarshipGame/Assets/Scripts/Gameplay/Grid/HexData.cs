using System;
using UnityEngine;

public enum HexType
{
    Default,
    Occupied
}

/// <summary>
/// Holds the data of every hexagon
/// </summary>
[SelectionBase]
public class HexData : MonoBehaviour
{
    [SerializeField] private GlowManager highlight;
    [SerializeField] private HexType hexType;
    public Vector2Int Grid;

    private void Awake()
    {
        highlight = GetComponent<GlowManager>();
    }

    /// <summary>
    /// Switch Case. Hex of type Default have a value of 1, every other type is not supported
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int GetType()
    {
        switch (hexType)
        {
            case HexType.Default:
                return 1;
            default:
                throw new Exception($"Hex of type{hexType} not supported");
        }   
    }

    public bool IsObstacle()
    {
        return hexType == HexType.Occupied;
    }

    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

    public void ResetHighlight()
    {
        highlight.ResetSelectedHighlight();
    }

    public void HighlightPath()
    {
        highlight.HighlightValidPath();
    }
}