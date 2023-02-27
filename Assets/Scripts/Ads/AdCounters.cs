using UnityEngine;

public class AdCounters : MonoBehaviour
{
    public int ColorRewardedAdCounter = 1;
    public int InterstitialAdCounter = 1;

    public static AdCounters Instance;
    
    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
