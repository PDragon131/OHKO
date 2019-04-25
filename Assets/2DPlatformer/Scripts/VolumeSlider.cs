using UnityEngine;
using System.Collections;

public class VolumeSlider : MonoBehaviour
{
    private float startVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            startVolume = PlayerPrefs.GetFloat("volume");
            AdjustVolume(startVolume);
        }
    }

    private void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

}