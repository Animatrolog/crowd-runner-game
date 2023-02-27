using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitGravity))]
public class UnitAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _distancePerSecond;

    private UnitMovement _unitMovement;
    private UnitGravity _unitGravity;

    private void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
        _unitGravity = GetComponent<UnitGravity>();
        _animator.SetFloat("cycleOffset", Random.Range(0f, 1f));
    }

    public void LookAtDiection( Vector3 lookDirection)
    {
        _animator.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        _animator.SetBool("isFalling", !_unitGravity.IsGrounded);
        _animator.SetBool("isRunning", _unitMovement.IsRunning);
    }
}
