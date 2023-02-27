using UnityEngine;

[RequireComponent(typeof(CrowdUnitManager))]
public class CrowdBoxColliderScaller : MonoBehaviour
{
    [SerializeField] private BoxCollider _crowdCollider;

    private CrowdUnitManager _unitManager;
    
    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable() => _unitManager.OnCountChanged += ResizeCollider;

    private void OnDisable() => _unitManager.OnCountChanged -= ResizeCollider;
    
    private void ResizeCollider(int unitCount)
    {
        _crowdCollider.size = new Vector3(10f, _crowdCollider.size.y, _unitManager.CrowdRadius * 2);
    }
}
