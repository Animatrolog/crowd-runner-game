using UnityEngine;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _poolOfTraps;
    [SerializeField] private List<Transform> _trapPoints;
    [SerializeField] private bool _allwaysSpawnOne;
    private int _trapsCount;

    private void Start()
    {
        _trapsCount = 0;
        SpawnTraps();
        while(_allwaysSpawnOne && _trapsCount < 1)
        {
            SpawnTraps();
        }
    }

    private void SpawnTraps()
    {
        for(int i = 0; i < _trapPoints.Count; i++)
        {
            GameObject randomTrap = SpawnRandomTrap(_trapPoints[i]);
            if(randomTrap != null)
                _trapsCount++;
        }    
    }

    private GameObject SpawnRandomTrap(Transform pointTransform)
    {
        int prefabIndex = Random.Range(0, _poolOfTraps.Count + 1);
        if (prefabIndex > _poolOfTraps.Count - 1) 
            return null;
        else
            return Instantiate(_poolOfTraps[prefabIndex], pointTransform);
    }
}
