using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        InitializeReferences();
        Debug.Log(audioSource.volume);
    }

    private void InitializeReferences()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ModifyVolume(bool modifyVolume)
    {
        if (modifyVolume)
            audioSource.volume += 0.1f;
        if (!modifyVolume)
            audioSource.volume -= 0.1f;
        AdjustVolume();
        Debug.Log("Current Volume : " + audioSource.volume);
    }

    private void AdjustVolume()
    {
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
    }
}