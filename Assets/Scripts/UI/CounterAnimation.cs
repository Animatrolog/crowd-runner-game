using System.Collections;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private Vector3 _targetScale;

    private bool _isAnimating = false;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private IEnumerator AnimateCorutine()
    {
        _isAnimating = true;
        while(transform.localScale != _originalScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _originalScale, Time.deltaTime * _animationSpeed);
            yield return null;
        }
        _isAnimating = false;
    }

    public void Animate(int count)
    {
        if (_isAnimating) return;
        transform.localScale = _targetScale;
        StartCoroutine(AnimateCorutine());
    }
}
