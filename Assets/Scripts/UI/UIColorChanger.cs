using UnityEngine;
using UnityEngine.UI;

public class UIColorChanger : MonoBehaviour
{
    [SerializeField] private Image _backgroungImage;

    public void SetColor(Color color)
    {
        Color unitColor = color;
        _backgroungImage.color = new Color(unitColor.r, unitColor.g, unitColor.b, _backgroungImage.color.a);
    }
}
