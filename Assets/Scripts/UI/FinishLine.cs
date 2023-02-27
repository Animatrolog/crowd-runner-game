using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Vector3 _finishPosition;

    public UnityEvent OnFinish;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<CrowdMovement>(out CrowdMovement crowd))
        {
            GameStateManager.Instance.SetState(GameState.Finish);
            crowd.TargetPosition = _finishPosition;
            crowd.MoveSpeed = 1f;
            OnFinish?.Invoke();
        }  
    } 

}
