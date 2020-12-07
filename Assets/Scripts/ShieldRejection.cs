using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRejection : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "BulletEnemy"){
            Destroy(other.gameObject);

            
            // other.gameObject.tag = "PlayerBullet"; Нереализованная идея того, что пули отражаются обратно во врага.
            // other.gameObject.transform.Translate(new Vector3(other.gameObject.transform.position.x + 10,other.gameObject.transform.position.y))
        }
    }
}
