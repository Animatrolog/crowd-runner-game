using UnityEngine;

public class MoveToCanvasOnStart : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(GameObject.Find("UnitCounters").GetComponent<Transform>(), false);
    }
}
