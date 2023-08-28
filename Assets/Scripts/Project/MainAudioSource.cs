using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    public float stepVolume;
    private float volumeRounded;
    private AudioSource audioSource;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ShowVolume();
    }

    public void ModifyVolume(bool modifyVolume)
    {
        if (modifyVolume)
            audioSource.volume += stepVolume;
        else
            audioSource.volume -= stepVolume;
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
        ShowVolume();
    }

    private void ShowVolume()
    {
        volumeRounded = Mathf.Round(audioSource.volume * 100f) / 100f;
        Debug.Log("Current Volume : " + volumeRounded);
    }
}