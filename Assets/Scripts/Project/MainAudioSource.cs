using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    public Color classColor;
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
            ConsoleLog($"Volume Increased By {stepVolumeSize:F2}.");
        }
        else
        {
            audioSource.volume -= stepVolumeSize;
            ConsoleLog($"Volume Decreased By {stepVolumeSize:F2}.");
        }
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
        ShowVolume();
    }

    private void ShowVolume()
    {
        volumeRounded = Mathf.Round(audioSource.volume * 100f) / 100f;
        ConsoleLog($"Current Volume = {volumeRounded:F2} .");
    }
    private void ConsoleLog(string message)
    {
        gameManager.MainConsoleLog($"{message}", classColor);
    }
}