﻿using UnityEngine;

public class ShieldRejection : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BulletEnemy")
        {
            Destroy(other.gameObject);
        }
    }
}
