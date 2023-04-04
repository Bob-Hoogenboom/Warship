using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    private Dictionary<Renderer, Material[]> _selectionMaterialDictionary = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> _originalMaterialDictionary = new Dictionary<Renderer, Material[]>();
    private Dictionary<Color, Material> _cachedSelectionMaterials = new Dictionary<Color, Material>();

    public Material _selectionMaterial;

    public bool isGlowing = false;

    private void Awake()
    {
        prepareMaterialDictionaries();
    }

    private void prepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            _originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials = new Material[renderer.materials.Length];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if (!_cachedSelectionMaterials.TryGetValue(originalMaterials[i].color, out mat))
                {
                    mat = new Material(_selectionMaterial);
                    mat.color = originalMaterials[i].color;
                }

                newMaterials[i] = mat;
            }

            _selectionMaterialDictionary.Add(renderer, newMaterials);
        }
    }

    public void ToggleGlow()
    {
        if (!isGlowing)
        {
            foreach (Renderer renderer in _originalMaterialDictionary.Keys)
            {
                renderer.materials = _selectionMaterialDictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in _originalMaterialDictionary.Keys)
            {
                renderer.materials = _originalMaterialDictionary[renderer];
            }
        }

        isGlowing = !isGlowing;
    }

    public void ToggleGlow(bool state)
    {
        if (isGlowing == state) return;
        isGlowing = !state;
        ToggleGlow();
    }
}
