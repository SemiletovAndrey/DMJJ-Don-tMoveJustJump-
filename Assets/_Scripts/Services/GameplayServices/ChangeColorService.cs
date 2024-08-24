using UnityEngine;

public class ChangeColorService
{
    private Renderer[] _renderers;
    private MaterialPropertyBlock[] _propertyBlocks;
    private Color[] _originalColors;
    private Color _targetColor;

    public ChangeColorService(Renderer[] renderers, Color targetColor)
    {
        _renderers = renderers;
        _targetColor = targetColor;
        _propertyBlocks = new MaterialPropertyBlock[_renderers.Length];
        _originalColors = new Color[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            _propertyBlocks[i] = new MaterialPropertyBlock();
            _renderers[i].GetPropertyBlock(_propertyBlocks[i]);
            _originalColors[i] = _renderers[i].sharedMaterial.GetColor("_BaseColor");
            _propertyBlocks[i].SetColor("_BaseColor", _originalColors[i]);
        }
    }

    public void ChangeColor(float elapsed)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            Color newColor = Color.Lerp(_originalColors[i], _targetColor, elapsed + 0.5f);
            _propertyBlocks[i].SetColor("_BaseColor", newColor);
            _renderers[i].SetPropertyBlock(_propertyBlocks[i]);
        }
    }

    public void ResetColor()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _propertyBlocks[i].SetColor("_BaseColor", _originalColors[i]);
            _renderers[i].SetPropertyBlock(_propertyBlocks[i]);
        }
    }
}
