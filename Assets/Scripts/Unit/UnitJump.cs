using System.Collections;
using UnityEngine;

public class UnitJump : MonoBehaviour
{
    [SerializeField] AnimationCurve _jumpCurve;

    public bool IsJumping { get; private set; }

    public void Jump(float jumpHight, float jumpDuration)
    {
        if (IsJumping) return;
        float randomAdjust = Random.Range(-0.3f, 0.3f);
        StartCoroutine(AnimateJump(jumpHight + randomAdjust, jumpDuration));
    }

    private IEnumerator AnimateJump(float jumpHight, float jumpDuration)
    {
        IsJumping = true;
        float progress = 0;
        float jumpStartY = transform.position.y;
        while (progress < 1)
        {
            progress += Time.deltaTime / jumpDuration;
            float jumpY = _jumpCurve.Evaluate(progress) * jumpHight;
            transform.position = new(transform.position.x, jumpStartY + jumpY, transform.position.z);
            yield return null;
        }
        IsJumping = false;
    }
}
