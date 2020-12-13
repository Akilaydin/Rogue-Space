using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    private BoxCollider2D boundCollider; //Ссылка на наш бокс коллайдер
    private Vector2 viewPortSize; //Размеры камеры

    private void Awake(){
        boundCollider = GetComponent<BoxCollider2D>();
    }

    private void Start(){
        ResizeCollider();
    }

    void ResizeCollider(){
        viewPortSize = Camera.main.ViewportToWorldPoint(new Vector2(1,1) * 2); //Здесь получаем размер, я не совсем понимаю, как это работает.
        viewPortSize.x *= 1.5f;
        viewPortSize.y *= 1.5f;

        boundCollider.size = viewPortSize;
    }

    public void OnTriggerExit2D(Collider2D coll){
        switch(coll.tag){
            case "Planet":
                Destroy(coll.gameObject);
                break;
            case "BulletEnemy":
                Destroy(coll.gameObject);
                break;
            case "PlayerBullet":
                Destroy(coll.gameObject);
                break;
            case "Player":
                Destroy(coll.gameObject);
                break;
            case "Enemy":
                Destroy(coll.gameObject);
                break;
            case "Guns":
                Destroy(coll.gameObject);
                break;
            case "Bomb":
                Destroy(coll.gameObject);
                break;
            case "Shield":
                Destroy(coll.gameObject);
                break;
            case "HP":
                Destroy(coll.gameObject);
                break;
        }
    }
}
