using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundsSettings : MonoBehaviour
{
    public GameObject parentCanvas;
    public AudioMixer masterMixer,musicMixer,effectMixer;
    private float prefsMaster, prefsMusic, prefsEffects;
    private Slider masterSlider,musicSlider, effectSlider;


    void Start(){

        effectSlider = FindObject(parentCanvas,"EffectVolumeSlider").GetComponent<Slider>();
        musicSlider = FindObject(parentCanvas,"MusicVolumeSlider").GetComponent<Slider>();
        masterSlider = FindObject(parentCanvas,"MasterVolumeSlider").GetComponent<Slider>();


        if (PlayerPrefs.HasKey("masterVolume")){
            prefsMaster = PlayerPrefs.GetFloat("masterVolume",0);
            masterMixer.SetFloat("masterVolume",prefsEffects);
            masterSlider.value = prefsMaster;
        }

        if (PlayerPrefs.HasKey("musicVolume")){
            prefsMusic = PlayerPrefs.GetFloat("musicVolume",0);
            musicMixer.SetFloat("musicVolume",prefsMusic);
            musicSlider.value = prefsMusic;
        }

        if (PlayerPrefs.HasKey("effectVolume")){
            prefsEffects = PlayerPrefs.GetFloat("effectVolume",0);
            effectMixer.SetFloat("effectVolume",prefsEffects);
            effectSlider.value = prefsEffects;
        }

        
    }

    public void setVolumeToMusic(float volume){
        musicMixer.SetFloat("musicVolume",volume);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }

    public void setVolumeToEffects(float volume){
        effectMixer.SetFloat("effectVolume",volume);
        PlayerPrefs.SetFloat("effectVolume",volume);
    }

    public void setVolumeToMaster(float volume){
        masterMixer.SetFloat("masterVolume",volume);
        PlayerPrefs.SetFloat("masterVolume",volume);
    }





    public static GameObject FindObject(GameObject parent, string name)
    {
     Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
        if(t.name == name){
              return t.gameObject;
        }
     }
     return null;
    }

}
