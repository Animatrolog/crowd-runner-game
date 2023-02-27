using UnityEngine;

public class CrowdMovement : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] public float MoveSpeed;
    [SerializeField] private float _minXPosition;
    [SerializeField] private float _maxXPosition;

    private Vector3 _targetPosition;

    public Vector3 CrowdDirection { get; private set; }

    public Vector3 TargetPosition
    {
        get => _targetPosition; 

        set
        {
            _targetPosition = new Vector3(Mathf.Clamp(value.x, _minXPosition, _maxXPosition), value.y, value.z);
        }
    }

    private void Start()
    {
        TargetPosition = transform.position;
        CrowdDirection = transform.forward * 0.1f;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == TargetPosition) return;
        
        Vector3 diffVector = TargetPosition - transform.position;
        Vector3 runVector = _runSpeed * Mathf.Clamp(diffVector.z, 0f, 1f) * Vector3.forward;
        Vector3 moveVector = MoveSpeed * diffVector.x * Vector3.right;

        CrowdDirection = runVector + moveVector;
        transform.Translate(Time.deltaTime * CrowdDirection);
    }
}
