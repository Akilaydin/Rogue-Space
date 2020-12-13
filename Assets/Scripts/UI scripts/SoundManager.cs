using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] sources;
    private int sourceIndex = -1;
    private bool anyAudioIsPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
        StartCoroutine(PlaySoundtrack());
        Debug.Log(sources.Length);
    }

    private IEnumerator PlaySoundtrack(){
        ReverseTheOrderOfSongs();
        if (CheckIfAnyAudioIsPlaying() == false){
            Debug.Log("If branch in the PlaySoundThack Coroutine");
            sourceIndex++;
            sources[sourceIndex].Play();
            Debug.Log(sources[sourceIndex].clip.length);
            yield return new WaitForSeconds (sources[sourceIndex].clip.length);
        } else {
            Debug.Log("Else branch in the PlaySoundThack Coroutine");
            yield return new WaitForSeconds (15);
        }
    }

    private bool CheckIfAnyAudioIsPlaying(){
        foreach (var source in sources){
            if (source.isPlaying)
                anyAudioIsPlaying = true;
        }
        return anyAudioIsPlaying;
    }

    private void ReverseTheOrderOfSongs(){
        if (sourceIndex > sources.Length){
            sourceIndex = 0;
        }
    }


    
}
