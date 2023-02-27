using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdImageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _priceImage;
    [SerializeField] private Sprite _adSprite;
    [SerializeField] private Sprite _coinSprite;

    public void SetRewarded(bool isRewarded, int price = 0)
    {
        if(isRewarded)
        {
            _priceText.text = "Watch Ads";
            _priceImage.sprite = _adSprite;
        }
        else
        {
            if (price == 0) _priceText.text = "Free";
            else _priceText.text = price.ToString();
            _priceImage.sprite = _coinSprite;
        }
    }
}
