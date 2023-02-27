using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private RotationAxis _rotationAxis;

    private Vector3 _rotationVector;

    private void Start()
    {
        switch(_rotationAxis)
        {
            case RotationAxis.Up:
                _rotationVector = Vector3.up;
                break;
            case RotationAxis.Right:
                _rotationVector = Vector3.right;
                break;
            case RotationAxis.Froward:
                _rotationVector = Vector3.forward;
                break;
        }
    }

    void Update()
    {
        transform.Rotate(_speed * Time.deltaTime * _rotationVector);
    }
}

public enum RotationAxis
{
    Up = 0,
    Right = 1,
    Froward = 2
}
