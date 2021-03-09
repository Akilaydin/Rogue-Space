using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bonus : MonoBehaviour
{

    public float shieldLasting = 4f;
    public ParticleSystem bombPS;
    private float shieldDec;
    private GameObject shieldSprite;
    private Slider shieldUISlider;
    private Slider hpUISlider;

    void Start()
    {
        shieldSprite = GameObject.FindGameObjectWithTag("ShieldSprite");
        shieldUISlider = GameObject.FindGameObjectWithTag("Shield_UI").GetComponent<Slider>();
        hpUISlider = GameObject.FindGameObjectWithTag("HP_UI").GetComponent<Slider>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            switch (gameObject.tag)
            {
                case "Guns":
                    if (PlayerShooting.instance.currentLevelOfGuns < PlayerShooting.instance.maxLevelOfGuns)
                    {
                        PlayerShooting.instance.currentLevelOfGuns++;
                    }
                    break;

                case "Shield":
                    shieldSprite.GetComponent<EdgeCollider2D>().enabled = true;
                    shieldSprite.GetComponent<SpriteRenderer>().enabled = true;
                    shieldUISlider.fillRect.gameObject.SetActive(true);
                    Invoke("ShieldsDown", shieldLasting);
                    shieldUISlider.value = 1f;
                    InvokeRepeating("DecShieldMeter", 0, shieldLasting / 100);
                    break;

                case "HP":
                    if (Player.instance.playerHealth < Player.instance.playerMaxHealth)
                    {
                        Player.instance.playerHealth++;
                        hpUISlider.value = (float)Player.instance.playerHealth / Player.instance.playerMaxHealth;
                    }

                    break;
                case "Bomb":
                    foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        enemy.GetComponent<Enemy>().GetDamage(10);
                    }
                    foreach (var bullet in GameObject.FindGameObjectsWithTag("BulletEnemy"))
                    {
                        Destroy(bullet);
                    }
                    Instantiate(bombPS, transform.position, Quaternion.identity);
                    break;
            }
            gameObject.GetComponent<AudioSource>().Play();
            transform.position = new Vector2(transform.position.x + 150, transform.position.y - 150); //Просто выкидываю за сцену, а потом уничтожаю. Так нужно потому, что инвоук перестает работать, если объект уничтожен.
            Destroy(gameObject, 6f);
        }


    }
    void DecShieldMeter()
    {
        if (shieldUISlider.value <= 0)
        {
            CancelInvoke("DecShieldMeter");
        }
        shieldUISlider.value -= 0.01f;

    }
    void ShieldsDown()
    {
        shieldSprite.GetComponent<EdgeCollider2D>().enabled = false;
        shieldSprite.GetComponent<SpriteRenderer>().enabled = false;
        shieldUISlider.fillRect.gameObject.SetActive(false); //Заставляет ползунок со щитом исчезнуть
    }
}
