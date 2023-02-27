using UnityEngine;

public class LevelPieaceScaler : MonoBehaviour
{
    private void Awake()
    {
        float randomX = 1;
        if (Random.Range(0, 2) > 0)
            randomX = -1;
        transform.localScale = new(randomX, 1, 1);    
    }
}
