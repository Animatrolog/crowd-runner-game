using UnityEngine;

public class CoinFXAnimation : MonoBehaviour
{
    [SerializeField] Vector3 _offset;

    private GameObject _targetObject;
    private float _speed = 3f;

    private bool _initialized;

    void Update()
    {
        if (!_initialized) return;

        Vector3 targetPosition = _targetObject.transform.position + _offset;
        Quaternion targetRotation = _targetObject.transform.rotation;
        Vector3 targetScale = _targetObject.transform.localScale;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * _speed);
        transform.rotation = targetRotation;

        if (Vector3.Distance(transform.position, targetPosition) <= 0.2f)
            Destroy(gameObject);
    }

    public void InitFXCoin(GameObject target)
    {
        _targetObject = target; 
        _initialized = true;
    }
}
