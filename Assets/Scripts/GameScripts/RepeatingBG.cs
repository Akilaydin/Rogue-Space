using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    public float verticalSize;    //Высота спрайта в пикселях

    private Vector2 offsetUp; //То, на сколько спрайт должен подняться вверх для нового использования

    private void Update(){
        if (transform.position.y < -verticalSize) {
            RepeatBackground();
        }
    }

    void RepeatBackground(){
        offsetUp = new Vector2(0,verticalSize * 2f);
        transform.position = (Vector2)transform.position + offsetUp;
    }
}
