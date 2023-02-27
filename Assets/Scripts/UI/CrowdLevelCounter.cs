using UnityEngine;
using TMPro;

public class CrowdLevelCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] CrowdUnitManager _unitManager;

    private void OnEnable() => _unitManager.OnCountChanged += UpdateText;

    private void OnDisable() => _unitManager.OnCountChanged -= UpdateText;

    private void UpdateText(int lvl)
    {
        _lvlText.text = lvl.ToString();
    }

}
