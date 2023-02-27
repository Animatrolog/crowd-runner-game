using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBlock : MonoBehaviour
{
    [SerializeField] private TMP_Text _requiredText;
    [SerializeField] private GameObject _disabledImage;
    [SerializeField] private Button _upgrdeButton;
    [SerializeField] private GameObject _rewardedPanel;

    public void SetBlock(bool hasEnoughCoins, bool hasEnoughLevel, int requiredLevel)
    {
        bool canUpgrade = hasEnoughCoins && hasEnoughLevel;

        _upgrdeButton.interactable = canUpgrade;
        _disabledImage.SetActive(!canUpgrade);

        _rewardedPanel.SetActive(hasEnoughLevel && !hasEnoughCoins);

        if (!hasEnoughLevel)
            _requiredText.text = ("Level " + requiredLevel + " required");
        else
            _requiredText.text = "Not enough coins";
    }

}
