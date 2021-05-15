using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    private int clipIndex;
    private bool anyAudioIsPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clipIndex = Random.Range(0, clips.Length);
        StartCoroutine(PlaySoundtrack());
    }

    private IEnumerator PlaySoundtrack()
    {
        while (true)
        {
            if (CheckIfAnyAudioIsPlaying() == false)
            {
                ReverseTheOrderOfSongs();
                source.clip = clips[clipIndex];
                clipIndex++;
                source.Play();
                yield return new WaitForSeconds(source.clip.length);
            }
            else
            {
                yield return new WaitForSeconds(5f);
            }
        }
    }

    private bool CheckIfAnyAudioIsPlaying()
    {
        if (source.isPlaying)
        {
            anyAudioIsPlaying = true;
        }
        return anyAudioIsPlaying;
    }

    private void ReverseTheOrderOfSongs()
    {
        if (clipIndex > clips.Length)
        {
            clipIndex = 0;
        }
    }
}
