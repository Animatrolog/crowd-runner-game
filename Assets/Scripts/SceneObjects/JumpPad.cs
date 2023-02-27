using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float _jumpHight;
    [SerializeField] float _jumpDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<UnitJump>(out UnitJump unit))
            unit.Jump(_jumpHight, _jumpDuration);
    }
}
