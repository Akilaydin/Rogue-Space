using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestroyer : MonoBehaviour
{
    private GameObject[] goParticles;

    [SerializeField]
    private float timeParticlesDestroy = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ParticleFinderAndDestroyer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ParticleFinderAndDestroyer()
    { //Сначала находим GO со спец. тэгом, потом вытаскиваем из них партикл системы, проверяем, проигрываются они или нет, и уничтожаем, если нет.
        List<ParticleSystem> particles = new List<ParticleSystem>();
        while (true)
        {
            goParticles = GameObject.FindGameObjectsWithTag("FXtoDestroy");//go stands for GameObject
            particles = GetParticlesFromGameObject(goParticles);
            DestroyParticles(particles);
            yield return new WaitForSeconds(timeParticlesDestroy);
        }
    }

    List<ParticleSystem> GetParticlesFromGameObject(GameObject[] goParticles)
    {
        List<ParticleSystem> particles = new List<ParticleSystem>();
        foreach (var goParticle in goParticles)
        { //go stands for GameObject
            particles.Add(goParticle.GetComponent<ParticleSystem>());
        }
        return particles;
    }

    void DestroyParticles(List<ParticleSystem> particles)
    {
        foreach (var particle in particles)
        {
            if (particle.isPlaying == false)
            {
                Destroy(particle.gameObject);
            }
        }

    }
}
