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
    [SerializeField] private GlowManager _highlight;
    [SerializeField] private HexType hexType;
    public Vector2Int Grid;

    private void Awake()
    {
        _highlight = GetComponent<GlowManager>();
    }

    public int GetType() => hexType switch
    {
        HexType.Default => 1,
        _ => throw new Exception($"Hex of type{hexType} not supported")
    };

    public bool IsObstacle()
    {
        return this.hexType == HexType.Occupied;
    }

    public void EnableHighlight()
    {
        _highlight.ToggleGlow(true);
    }
    
    public void DisableHighlight()
    {
        _highlight.ToggleGlow(false);
    }

    public void ResetHighlight()
    {
        _highlight.ResetSelectedHighlight();
    }

    public void HighlightPath()
    {
        _highlight.HighlightValidPath();
    }
}