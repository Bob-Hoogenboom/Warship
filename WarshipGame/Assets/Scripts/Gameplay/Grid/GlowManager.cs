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

    private bool isGlowing = false;

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
        }
    }
}
