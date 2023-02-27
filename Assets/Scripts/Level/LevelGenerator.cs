using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int _levelLenght;
    [SerializeField] private List<LevelPieace> _poolOfPrefabs;
    [SerializeField] private LevelPieace _firstPiece;
    [SerializeField] private Vector3 _offset;

    private int _level;
    private GameDataManager _gameDataManager;
    private int _previousIndex;
    private int _potencialUnitCount; 

    public void NextLevel()
    {
        _level++;
        _gameDataManager.Level = _level;
    }

    private void Awake()
    {
        _gameDataManager = GameDataManager.Instance;
    }

    private void Start()
    {
        LoadLevelData();
        _potencialUnitCount = (int)(_level * 0.1f) + 1;
        //Debug.Log("Start unit count = " + _potencialUnitCount);
        GenerateLevel();
    }

    private void LoadLevelData()
    {
        if (_gameDataManager != null)
            _level = _gameDataManager.Level;
    }

    private void GenerateLevel()
    {
        Random.InitState(_level);
        for (int i = 1; i < _levelLenght; i++ )
        {
            if ( i == 1 || i == 5 )
            {
                SpawnPiece(_firstPiece, i);
                continue;
            }
            SpawnRandomPiece(i);
        }
        //Debug.Log("Level generated, potential unit count  = " + _potencialUnitCount);
    }

    private void SpawnRandomPiece(int index)
    {
        int prefabIndex = Random.Range(0, _poolOfPrefabs.Count);
        
        while (prefabIndex == _previousIndex)
            prefabIndex = Random.Range(0, _poolOfPrefabs.Count);

        _previousIndex = prefabIndex;
        SpawnPiece(_poolOfPrefabs[prefabIndex], index);

    }

    private void SpawnPiece(LevelPieace piecePrefab, int index)
    {
        LevelPieace piece = Instantiate(piecePrefab, _offset * index, Quaternion.identity, transform);
        _potencialUnitCount = piece.Initialize(_level, index, _potencialUnitCount);
        //Debug.Log("potential unit count at " + index + " = " + _potencialUnitCount);
    }

}
