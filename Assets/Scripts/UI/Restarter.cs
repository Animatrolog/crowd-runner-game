
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public void Restart()
    {
        InterstitialAdHandler.Instance.ShowAd();
        SceneManager.LoadScene(1);
    }
}
