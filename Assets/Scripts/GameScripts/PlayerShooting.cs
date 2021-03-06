﻿using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public static PlayerShooting instance;
    public GameObject bulletObj;
    public float fireRate = 0.3f; //Перезарядка
    [HideInInspector]
    public float timer_Shot;
    private AudioSource shootAudio = null;
    public int damage = 1;

    [Header("GunsSettings")]
    public ParticleSystem gunCPS, gunRPS, gunLPS, gunLLPS, gunRRPS;
    public GameObject gunC, gunR, gunL, gunLL, gunRR;
    public int currentLevelOfGuns = 1; //Дописывал сам
    public int maxLevelOfGuns = 5; //Дописывал сам

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        shootAudio = PlayerSoundManager.instance.GetAudioByClipName("ShotSound");
        fireRate = Database.instance.LoadCurrentFireRate();
    }


    void Update()
    {
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + fireRate;
            switch (currentLevelOfGuns)
            {
                case 1:
                    Instantiate(bulletObj, gunC.transform.position, Quaternion.identity);
                    gunCPS.Play();
                    break;
                case 2:
                    Instantiate(bulletObj, gunC.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunR.transform.position, Quaternion.identity);
                    gunCPS.Play();
                    gunRPS.Play();
                    break;
                case 3:
                    Instantiate(bulletObj, gunC.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunR.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunL.transform.position, Quaternion.identity);
                    gunCPS.Play();
                    gunRPS.Play();
                    gunLPS.Play();
                    break;
                case 4:
                    Instantiate(bulletObj, gunC.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunR.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunL.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunRR.transform.position, Quaternion.identity);
                    gunCPS.Play();
                    gunRPS.Play();
                    gunLPS.Play();
                    gunRRPS.Play();
                    break;
                case 5:
                    Instantiate(bulletObj, gunC.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunR.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunL.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunRR.transform.position, Quaternion.identity);
                    Instantiate(bulletObj, gunLL.transform.position, Quaternion.identity);
                    gunCPS.Play();
                    gunRPS.Play();
                    gunLPS.Play();
                    gunRRPS.Play();
                    gunLLPS.Play();
                    break;
            }


            if (shootAudio != null)
            {
                shootAudio.pitch = Random.Range(0.8f, 1.2f);
                shootAudio.Play();
            }
            else
            {
                Debug.Log("sHA == null");
            }



        }
    }

}
