using UnityEngine;
using UnityEngine.EventSystems;
public class SoundPlayOnClick : MonoBehaviour, IPointerDownHandler
{
    private AudioSource source;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        source.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayClip();
    }
}
