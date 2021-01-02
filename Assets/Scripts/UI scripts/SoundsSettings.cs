using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundsSettings : MonoBehaviour
{
    public GameObject parentCanvas;
    public AudioMixer masterMixer, musicMixer, effectMixer;
    private float prefsMaster, prefsMusic, prefsEffects;
    private Slider masterSlider, musicSlider, effectSlider;
    private string MASTER_VOLUME = "masterVolume";
    private string MUSIC_VOLUME = "musicVolume";
    private string EFFECT_VOLUME = "effectVolume";

    void Start()
    {

        effectSlider = FindObject(parentCanvas, "EffectVolumeSlider").GetComponent<Slider>();
        musicSlider = FindObject(parentCanvas, "MusicVolumeSlider").GetComponent<Slider>();
        masterSlider = FindObject(parentCanvas, "MasterVolumeSlider").GetComponent<Slider>();


        if (PlayerPrefs.HasKey(MASTER_VOLUME))
        {
            prefsMaster = PlayerPrefs.GetFloat(MASTER_VOLUME, 0);
            masterMixer.SetFloat(MASTER_VOLUME, prefsMaster);
            masterSlider.value = prefsMaster;
        }

        if (PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            prefsMusic = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0);
            musicMixer.SetFloat(MUSIC_VOLUME, prefsMusic);
            musicSlider.value = prefsMusic;
        }

        if (PlayerPrefs.HasKey(EFFECT_VOLUME))
        {
            prefsEffects = PlayerPrefs.GetFloat(EFFECT_VOLUME, 0);
            effectMixer.SetFloat(EFFECT_VOLUME, prefsEffects);
            effectSlider.value = prefsEffects;
        }


    }
    public void setVolumeToMaster(float volume)
    {
        masterMixer.SetFloat(MASTER_VOLUME, volume);
        PlayerPrefs.SetFloat(MASTER_VOLUME, volume);
    }
    public void setVolumeToMusic(float volume)
    {
        musicMixer.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
    }

    public void setVolumeToEffects(float volume)
    {
        effectMixer.SetFloat(EFFECT_VOLUME, volume);
        PlayerPrefs.SetFloat(EFFECT_VOLUME, volume);
    }


    private GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}
