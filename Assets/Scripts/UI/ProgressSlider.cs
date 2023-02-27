using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    [SerializeField] private Transform _mainCrowdTransform;
    [SerializeField] private Transform _finishTransform;
    [SerializeField] private Slider _slider;

    void Start()
    {
        _slider.maxValue = _finishTransform.position.z;
      
    }

    private void UpdateProgress()
    {
        _slider.value = _mainCrowdTransform.position.z;
    }

    void Update()
    {
        UpdateProgress();
    }
}
