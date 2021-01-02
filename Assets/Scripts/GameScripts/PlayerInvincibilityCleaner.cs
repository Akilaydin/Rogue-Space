using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibilityCleaner : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                other.GetComponent<Enemy>().GetDamage(1);
                break;
            case "BulletEnemy":
                other.GetComponent<Bullet>().Destruction();
                break;
        }
    }
}
