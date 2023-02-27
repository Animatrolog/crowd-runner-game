using UnityEngine;

public class CrowdColorChanger : MonoBehaviour
{
    [SerializeField] private Color _crowdColor;
    [SerializeField] private Material _unitMaterial;
    [SerializeField] private Material _splashParticleMaterial;
    [SerializeField] private UIColorChanger _counterPanel;

    public Color CrowdColor => _crowdColor;

    private void Awake()
    {
        SetColor(_crowdColor);
    }

    public void SetColor(Color color)
    {
        _unitMaterial.color = color;
        _splashParticleMaterial.color = color;
        _counterPanel.SetColor(color);
    }
}
