using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer master;
    public AudioMixer fx;
    
    public void SetMasterVolume( float volume)
    {
        SetVolume(master, volume);
    }

    public void SetFxVolume(float volume)
    {
        SetVolume(fx, volume);
    }

    public void SetVolume(AudioMixer mixer, float volume)
    {
        mixer.SetFloat("Volume", Mathf.Log(volume) * 20);
    }
}
