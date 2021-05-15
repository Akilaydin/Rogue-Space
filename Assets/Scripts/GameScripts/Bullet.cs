using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float enemyDamage = 1;
    public float playerDamage = 1;
    public bool isEnemyBullet;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (isEnemyBullet && coll.tag == "Player" && Player.instance.isInvincible == false)
        {
            Player.instance.GetDamage(enemyDamage);
            Destroy(gameObject);
        }
        else if (!isEnemyBullet && coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().GetDamage(playerDamage);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (isEnemyBullet == false)
        {
            playerDamage = Database.instance.LoadCurrentDamage();
        }
    }

    public void Destruction()
    {
        Destroy(gameObject);
    }
}
