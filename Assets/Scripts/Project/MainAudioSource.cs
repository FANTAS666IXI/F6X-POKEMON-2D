using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    public float stepVolumeSize;
    private float volumeRounded;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ModifyVolume(bool modifyVolume)
    {
        if (modifyVolume)
        {
            audioSource.volume += stepVolumeSize;
            gameManager.ConsoleLog($"Volume Increased By {stepVolumeSize:F2}.");
        }
        else
        {
            audioSource.volume -= stepVolumeSize;
            gameManager.ConsoleLog($"Volume Decreased By {stepVolumeSize:F2}.");
        }
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
        ShowVolume();
    }

    private void ShowVolume()
    {
        volumeRounded = Mathf.Round(audioSource.volume * 100f) / 100f;
        gameManager.ConsoleLog($"Current Volume = {volumeRounded:F2} .");
    }
}