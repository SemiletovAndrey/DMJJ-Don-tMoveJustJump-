using System;
using UnityEngine;

[Serializable]
public class ChangeColorService
{
    private Renderer _renderers;
    private MaterialPropertyBlock _propertyBlocks;
    [SerializeField] private Color _originalColors;
    [SerializeField] private Color _targetColor = Color.red;
    [SerializeField] private Color _currentColor;

    public ChangeColorService(Renderer renderers)
    {
        _renderers = renderers;
        _propertyBlocks = new MaterialPropertyBlock();
        _renderers.GetPropertyBlock(_propertyBlocks);
        _originalColors = _renderers.sharedMaterial.GetColor("_Color");
        _currentColor = _originalColors;
        _propertyBlocks.SetColor("_Color", _originalColors);
    }

    public void ChangeColor(float elapsed)
    {
        _currentColor = _propertyBlocks.GetColor("_Color");
        Color newColor = Color.Lerp(_originalColors, _targetColor, elapsed + 0.5f);
        _propertyBlocks.SetColor("_Color", newColor);
        _renderers.SetPropertyBlock(_propertyBlocks);

    }

    public void ResetColor()
    {
        _currentColor = _originalColors;
        _propertyBlocks.SetColor("_Color", _originalColors);
        _renderers.SetPropertyBlock(_propertyBlocks);
    }
}
