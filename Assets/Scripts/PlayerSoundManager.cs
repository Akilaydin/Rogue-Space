using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager instance;
    private AudioSource[] playerAudioSources;

    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 

    public AudioSource GetAudioByClipName(string clipName)
    {
        playerAudioSources = gameObject.GetComponents<AudioSource>();
        foreach (var aud in playerAudioSources)
        {
            if (aud.clip.name == clipName)
            {                
               return aud;
            }
        }
        return null;
    }
}
