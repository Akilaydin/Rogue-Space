using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth;
    public int givenScore;

    [Space]
    public GameObject bulletObject;
    public float shotTimeMin, shotTimeMax; //Нужно, чтобы враг не стрелял тогда, когда игрок его не видит.
    public int shotChance; //Переменная для шанса на стрельбу
    public ParticleSystem enemyDeathPS, PlayerBulletPS;
    private AudioSource enemyDeathAudio;

    [Header("BOSS")]
    public bool isBoss;
    public bool isShootingBoss;
    public GameObject bulletBoss; //Super-attack
    public ParticleSystem bossDeathPS;

    public GameObject[] bulletWaves;
    public float superShotDelay; //Time_bullet_boss_spawn
    public int bossChanceShot; //Шанс на супер-выстрел
    public int bossWaveChanceShot;
    public int bossParticleCount; //Кол-во взрывов
    public float bossParticleDelay = 0.1f;
    private float bossTimerShot;
    private ParticleSystem[] bossDeathPSSPawnArray;
    private int bossParticleCountIterator; //Для последовательного взрыва партиклов у босса

    [Header("BonusEnemy")]
    public bool isBonusEnemy = false;
    public GameObject[] bonusObjets;
    [Range(0, 1)]
    public float chanceToGenerateBonus;




    void Start()
    {

        if (isShootingBoss)
        {
            Invoke("OpenFireBoss", 1);
        }
        if (isBoss)
        {
            bossDeathPSSPawnArray = bossDeathPS.GetComponentsInChildren<ParticleSystem>();
        }
        if (!isBoss)
        {
            enemyDeathAudio = gameObject.GetComponent<AudioSource>();
            InvokeRepeating("OpenFire", Random.Range(shotTimeMin, shotTimeMax), 3f);
        }
    }

    void Update()
    {
        if (isBoss)
        {
            if (Time.time > bossTimerShot)
            {
                bossTimerShot = Time.time + superShotDelay;
                OpenFire();
                OpenFireBoss();
            }
        }
    }
    void OpenFireBoss()
    {
        if ((Random.value < (float)bossWaveChanceShot / 100) && isBoss)
        {
            Instantiate(bulletWaves[Random.Range(0, bulletWaves.Length)], transform.position, Quaternion.identity);
        }
        if ((Random.value < (float)bossChanceShot / 100) && isBoss)
        {
            for (int zZz = -40; zZz < 40; zZz += 10)
            {
                Instantiate(bulletBoss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
        else if (isShootingBoss)
        {
            for (int zZz = -40; zZz < 40; zZz += 10)
            {
                Instantiate(bulletBoss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }


    void OpenFire()
    {
        if (Random.value < (float)shotChance / 100)
        { //Шанс на стрельбу
            Instantiate(bulletObject, transform.position, Quaternion.identity);
        }
    }


    public void GetDamage(float damage)
    {

        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            PlayEnemyDeathSound();
            EnemyDeath();

        }
        if (enemyHealth > 0)
        {
            StartCoroutine(GetTransparentOnDamage());
            Instantiate(PlayerBulletPS, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator GetTransparentOnDamage()
    {
        SpriteRenderer enemyRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g, enemyRenderer.color.b, 0.5f); //Выключаем
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g, enemyRenderer.color.b, 1);
    }

    private void PlayEnemyDeathSound()
    {

        if (enemyDeathAudio != null)
        {
            enemyDeathAudio.Play();
        }
        else
        {
            Debug.Log("EnemyDeathAudio == null");
        }
    }

    void BossDeath()
    {
        float xOffset = Random.Range(-2.5f, 2.5f);
        float yOffset = Random.Range(-2.5f, 2.5f);
        int particleNum = Random.Range(0, bossDeathPSSPawnArray.Length);

        if (bossParticleCountIterator < bossParticleCount)
        {
            Instantiate(bossDeathPSSPawnArray[particleNum], new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
            Invoke("BossDeath", bossParticleDelay);
            bossParticleCountIterator++;
        }
        else
        {
            LevelController.instance.ScoreInGame(givenScore);
            Destroy(gameObject);
        }
    }



    private void EnemyDeath()
    {

        LevelController.instance.ScoreInGame(givenScore);



        if (isBonusEnemy && Random.value <= chanceToGenerateBonus)
        {
            Instantiate(bonusObjets[Random.Range(0, bonusObjets.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (!isBoss)
        {

            Instantiate(enemyDeathPS, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (isBoss || isShootingBoss)
        { //Проверка на стреляющего босса(который появляется периодически в верху экрана), дабы избежать багов.
            gameObject.GetComponent<Enemy>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<PathFollowing>().enabled = false;
            BossDeath();
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GetDamage(1);
            Player.instance.GetDamage(1);
        }
    }
}
