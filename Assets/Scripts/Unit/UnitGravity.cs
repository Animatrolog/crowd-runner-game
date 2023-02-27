using UnityEngine;

public class UnitGravity : MonoBehaviour
{
    [SerializeField] private float _gravity = 5f;
    [SerializeField] private LayerMask _groundLayerMask;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        Ray ray = new(transform.position, Vector3.down);
        if (Physics.SphereCast(ray, 0.15f, out RaycastHit hit, 0.5f, _groundLayerMask))
        {
            transform.position = new(transform.position.x, hit.point.y + 0.5f, transform.position.z);
            IsGrounded = true;
        }
        else
        {
            transform.position += Time.deltaTime * _gravity * Vector3.down;
            IsGrounded = false;
        }
    }
}
