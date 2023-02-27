using UnityEngine;

public class FpsSettings : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 60;

    void Start()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
