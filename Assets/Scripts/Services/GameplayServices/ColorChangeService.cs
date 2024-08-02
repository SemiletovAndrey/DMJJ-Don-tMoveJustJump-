using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeService : MonoBehaviour
{
    private Renderer objectRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private Color _originalColor;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        _originalColor = objectRenderer.sharedMaterial.GetColor("_Color");
    }

    [ContextMenu("ChangeColor")]
    public void ChangeColor()
    {
        Color newColor = Color.red;
        // �������� ������� �������� ���������
        objectRenderer.GetPropertyBlock(materialPropertyBlock);

        // ������������� ����� ����
        materialPropertyBlock.SetColor("_Color", newColor);

        // ��������� �������� � ���������
        objectRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    [ContextMenu("Reset")]
    public void ResetColor()
    {
        materialPropertyBlock.SetColor("_Color", _originalColor);
        objectRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
