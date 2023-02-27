using UnityEngine;
using UnityEngine.Audio; 

public class UiSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;
    [SerializeField] private AudioClip _colorChangeClip;
    [SerializeField] private AudioClip _upgradeClip;
    [SerializeField] private AudioMixer _audioMixer;

    private GameDataManager _gameDataManager;

    public static UiSoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameDataManager = GameDataManager.Instance;
    }
    
    public void EnableSound(bool state)
    {
        float volume = -80f;

        if (state)
            volume = 0f;

        _audioMixer.SetFloat("Volume", volume);
    }

    public void PlayToggle(bool state)
    {
        if (state) PlayOpen();
        else PlayClose();
    }

    public void PlayOpen()
    {
        _audioSource.PlayOneShot(_openClip);
    }

    public void PlayClose()
    {
        _audioSource.PlayOneShot(_closeClip);
    }

    public void PlayColorChange()
    {
        _audioSource.PlayOneShot(_colorChangeClip);
        GameVibration.Vibrate();
    }

    public void PlayUpgrade()
    {
        _audioSource.PlayOneShot(_upgradeClip);
    }
}
