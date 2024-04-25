using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MM_ConnectSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    //private void Start()
    //{
    //    if (PlayerPrefs.GetInt("SaveSFX") == 1)
    //    {
    //        _SFXSound.value = PlayerPrefs.GetFloat("SFX");
    //    }
    //    _audioMixer.SetFloat("SFX", _SFXSound.value);
    //}

    public void SetVolumeGeneral(float volume)
    {
        audioMixer.SetFloat("General", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
