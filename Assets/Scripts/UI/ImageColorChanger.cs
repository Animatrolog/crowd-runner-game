using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    [SerializeField] private Image _image;

    private ColorChangeManager _colorManager;

    private void Start()
    {
        _colorManager = ColorChangeManager.Instance;
        _colorManager.OnColorChange += ChangeColor;
        ChangeColor(_colorManager.CrowdColor);
    }

    public void ChangeColor(Color color)
    {
        _image.color = new Color(color.r, color.g, color.b, 1f);
    }
}
