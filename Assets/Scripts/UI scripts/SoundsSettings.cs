using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundsSettings : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effectMixer;

    public void setVolumeToMusic(float volume){
        musicMixer.SetFloat("musicVolume",volume);
    }

    public void setVolumeToEffects(float volume){
        effectMixer.SetFloat("effectVolume",volume);
    }
}
