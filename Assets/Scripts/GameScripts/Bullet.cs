using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isEnemyBullet;
    void Start(){
        
    }
    private void OnTriggerEnter2D(Collider2D coll){
        if (isEnemyBullet && coll.tag == "Player" && Player.instance.isInvincible == false){
            Player.instance.GetDamage(damage);
            Destroy(gameObject);
        }
        else if (!isEnemyBullet && coll.tag == "Enemy"){
            coll.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }

    public void Destruction(){
        Destroy(gameObject);
    }
}
