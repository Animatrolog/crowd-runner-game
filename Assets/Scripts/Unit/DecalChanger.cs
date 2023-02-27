using UnityEngine;

public class DecalChanger : MonoBehaviour
{
    [SerializeField] private bool _randomizeRotation;
    [SerializeField] private bool _randomizeSize;
    [SerializeField] private MeshRenderer _decalMeshRenderer;

    private Material _decalMaterial;

    private void Awake()
    {
        _decalMaterial = _decalMeshRenderer.material;
    }

    public void SetColor(Color color)
    {
        _decalMaterial.color = color;
    }

    void Start()
    {
        if(_randomizeSize)
        {
            float randomSize = Random.Range(1f, 2f);
            transform.localScale = new Vector3(randomSize, 1f, randomSize);
        }

        if(_randomizeRotation)
        {
            int randomAngle = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, randomAngle, 0);
        }
    }
}
