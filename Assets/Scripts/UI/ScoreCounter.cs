using UnityEngine.Events;
using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private CrowdUnitManager _unitManager;
    [SerializeField] private StepPitch _stepPitch;
   
    private bool _isPlaying;
    private GameDataManager _gameDataManager;

    public UnityEvent OnCountUpdated;
    public UnityEvent OnAnimationFinished;

    private void OnEnable()
    {
        _gameDataManager = GameDataManager.Instance;
    }

    private void Start()
    { 
        Play(_unitManager.Count);    
    }

    private void UpdateCounter(int count)
    {
        _counterText.text = count.ToString();
    }

    private void Play(int unitCount)
    {
        if (_isPlaying) return;
        _stepPitch.Initialize(unitCount);
        _gameDataManager.Coins += unitCount; 
        StartCoroutine(AnimationCorutine(unitCount));
    }

    private IEnumerator AnimationCorutine(int count)
    {
        float waitTime = 0.05f;
        _isPlaying = true;
        for (int i = 0; i < count; i++)
        {
            UpdateCounter(i + 1);
            if ((i + 1) % 25 == 0) waitTime *= 0.75f;
            _stepPitch.PlayStep();
            OnCountUpdated?.Invoke();
            yield return new WaitForSeconds(waitTime);
        }
        OnAnimationFinished?.Invoke();
        _isPlaying = false;
    }
}
