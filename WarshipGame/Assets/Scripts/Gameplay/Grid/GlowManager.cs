using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages material colors and shaders to be activated and changed on selection.
/// </summary>
public class GlowManager : MonoBehaviour
{
    private readonly Dictionary<Renderer, Material[]> _selectionMaterialDictionary = new();
    private readonly Dictionary<Renderer, Material[]> _originalMaterialDictionary = new();
    private readonly Dictionary<Color, Material> _cachedSelectionMaterials = new();
    private readonly Color _validHexColor = Color.green;
    private Color _originalColor;
    
    public Material SelectionMaterial;
    public bool IsGlowing;
    
    private void Awake()
    {
        PrepareMaterialDictionaries();
        _originalColor = SelectionMaterial.GetColor("_GlowColor");
    }

    /// <summary>
    /// Fills in material dictionaries for later use 
    /// </summary>
    private void PrepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            _originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials = new Material[renderer.materials.Length];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                if (!_cachedSelectionMaterials.TryGetValue(originalMaterials[i].color, out var mat))
                {
                    mat = new Material(SelectionMaterial)
                    {
                        color = originalMaterials[i].color
                    };
                    _cachedSelectionMaterials.Add(originalMaterials[i].color, mat);
                }

                newMaterials[i] = mat;
            }

            _selectionMaterialDictionary.Add(renderer, newMaterials);
        }
    }

    /// <summary>
    /// Toggles the glow on or off when the function is called
    /// </summary>
    public void ToggleGlow()
    {
        if (!IsGlowing)
        {
            foreach (Renderer renderer in _originalMaterialDictionary.Keys)
            {
                renderer.materials = _selectionMaterialDictionary[renderer];
            }
            IsGlowing = true;
            return;
        }

        foreach (Renderer renderer in _originalMaterialDictionary.Keys)
        {
            renderer.materials = _originalMaterialDictionary[renderer];
        }

        IsGlowing = false;
    }

    /// <summary>
    /// Returns the state of the glow when the function is called
    /// </summary>
    /// <param name="state"></param>
    public void ToggleGlow(bool state)
    {
        if (IsGlowing == state) return;
        IsGlowing = !state;
        ToggleGlow();
    }

    /// <summary>
    /// Removes and reapplies the new highlight on the newly selected object
    /// </summary>
    internal void ResetSelectedHighlight()
    {
        if (!IsGlowing) return;
        foreach (Renderer renderer in _selectionMaterialDictionary.Keys)
        {
            foreach (Material item in _selectionMaterialDictionary[renderer])
            { 
                item.SetColor("_GlowColor", _originalColor);
            }
            renderer.materials = _selectionMaterialDictionary[renderer];
        }
    }

    /// <summary>
    /// Highlights a valid path, every hex that isn't valid does not get highlighted
    /// </summary>
    internal void HighlightValidPath()
    {
        if (!IsGlowing) return;
        foreach (Renderer renderer in _selectionMaterialDictionary.Keys)
        {
            foreach (Material item in _selectionMaterialDictionary[renderer])
            { 
                item.SetColor("_GlowColor", _validHexColor);
            }
        }
    }
}
