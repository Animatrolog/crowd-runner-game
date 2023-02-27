using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UnitCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private CrowdUnitManager _unitManager;

    public UnityEvent<int> OnAmountUpdated; 

    private void OnEnable()
    {
        _unitManager.OnCountChanged += UpdateCounter;
        _unitManager.OnAllUnitsDestroyed += HideCounter;
    }

    private void Start()
    {
        _counterText.text = _unitManager.Count.ToString();
    }

    private void OnDisable()
    {
        _unitManager.OnCountChanged -= UpdateCounter;
        _unitManager.OnAllUnitsDestroyed -= HideCounter;
    }

    private void UpdateCounter(int count)
    {
        _counterText.text = count.ToString();
        OnAmountUpdated?.Invoke(count);
    }

    private void HideCounter()
    {
        gameObject.SetActive(false);
    }
}
