using UnityEngine;

public class UITargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_targetToFollow == null || _camera == null) return;
        transform.position = _camera.WorldToScreenPoint(_targetToFollow.transform.position + _offset);
    }
}
