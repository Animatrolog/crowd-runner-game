using UnityEngine;

[RequireComponent(typeof(CrowdUnitManager))]
public class CrowdSphereColliderScaler : MonoBehaviour
{
    [SerializeField] private SphereCollider _crowdCollider;

    private CrowdUnitManager _unitManager;

    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable() => _unitManager.OnCountChanged += ResizeCollider;

    private void OnDisable() => _unitManager.OnCountChanged -= ResizeCollider;
    
    private void ResizeCollider(int unitCount)
    {
        _crowdCollider.radius = _unitManager.CrowdRadius;
    }
}
