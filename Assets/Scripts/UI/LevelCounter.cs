using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _additionalNumber;
    [SerializeField] private string _prefix;

    private GameDataManager _gameDataManager;

    void Start()
    {
        _gameDataManager = GameDataManager.Instance;
        _text.text = _prefix + (_gameDataManager.Level + _additionalNumber);
    }
}
