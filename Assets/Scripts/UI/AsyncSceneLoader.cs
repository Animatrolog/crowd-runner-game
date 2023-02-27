using UnityEngine.SceneManagement;
using UnityEngine;

public class AsyncSceneLoader : MonoBehaviour
{
    void Start()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(1);
    }
}
