using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Borders {
    public float minXOffset =1.1f; //left border offset
    public float maxXOffset = 1.1f; //right border offset
    public float minYOffset = 1.1f; //bottom border offset
    public float maxYOffset = 1.1f; //Top border offset

    [HideInInspector]
    public float minX,maxX,minY,maxY; //Переменные, нужные для создания границ перемещения игрока

}
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance; //Ссылка для того, чтобы другие объекты могли к нам обращаться.
    public Borders borders;
    public int playerSpeed = 5; 
    public int fingerInputOffset = 5;
    private Camera _camera;
    private Vector2 mousePosition; //Нужно, чтобы отслеживать, куда нажал игрок
    
    private void Awake(){
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        _camera = Camera.main;
    }

    void Start(){
        ResizeBorders();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePosition.x,mousePosition.y + fingerInputOffset), playerSpeed * Time.deltaTime);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x,borders.minX,borders.maxX),Mathf.Clamp(transform.position.y,borders.minY,borders.maxY));
        } 

    }

    private void ResizeBorders(){ //Метод для рассчета границ
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset; //Левая граница
        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset; //Нижняя граница
        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset; //Правая граница
        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset; //Верхняя граница
    }
}
