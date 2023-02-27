using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CrowdMovement _crowd;
    [SerializeField] private float _sensitivity;

    private Vector3 _targetHorizontalPosition;
    private Transform _cameraTransform;
    private Transform _crowdTransform;
    private float _distanceFromCamera;

    private void Start()
    {
        _cameraTransform = _camera.transform;
        _crowdTransform = _crowd.transform;
        _distanceFromCamera = Vector3.Distance(_cameraTransform.position, _crowdTransform.position);

        _targetHorizontalPosition = _crowdTransform.position.x * Vector3.right;
        _touchDeltaPos = Input.mousePosition;
    }

    void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            _touchDeltaPos = Input.mousePosition - _touchDeltaPos;
            if (Input.GetMouseButtonDown(0))
            {
                _targetHorizontalPosition = _crowdTransform.position.x * Vector3.right;
                _touchDeltaPos = Vector3.zero;
            }
            if(_touchDeltaPos.magnitude > 0)
                _targetHorizontalPosition += WorldPositionDelta().x * _sensitivity * Vector3.right;
            _touchDeltaPos = Input.mousePosition;
        }

        _crowd.TargetPosition = _targetHorizontalPosition + (_crowdTransform.position.z * Vector3.forward) + Vector3.forward;
    }

    Vector3 _touchDeltaPos = Vector3.zero;

    private Vector3 WorldPositionDelta()
    {
        Vector3 touchPosition = Input.mousePosition;

        if (_touchDeltaPos.magnitude == 0)
            return Vector3.zero;
        
        var posBefore = _camera.ScreenToWorldPoint(touchPosition - _touchDeltaPos + (Vector3.forward * _distanceFromCamera));//(touch.position - touch.deltaPosition);
        var posNow = _camera.ScreenToWorldPoint(touchPosition + (Vector3.forward * _distanceFromCamera));

        return posNow - posBefore;
    }
}
