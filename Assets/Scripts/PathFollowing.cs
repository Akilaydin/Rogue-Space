using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    [HideInInspector] public Transform[] pathPoints; //Путь для перемещения врага, состоящий из точек.
    [HideInInspector] public float enemySpeed;

    [HideInInspector] public bool isReturn; //Отвечает за то, будет ли враг возвращаться на первую точку после того, как пересечет последнюю.
   [HideInInspector] public Vector3[] newPositions;


    private int currentPointNum; 

    private void Start(){
        newPositions = NewPositionByPath(pathPoints);
        transform.position = newPositions[0]; //Отправляем в начало.
    }

    private void Update(){
        transform.position = Vector3.MoveTowards(transform.position,newPositions[currentPointNum],enemySpeed * Time.deltaTime); //Перемещаем врага
        if (Vector3.Distance(transform.position,newPositions[currentPointNum]) < 0.2f) { //Если враг долетел до следующей точки, но не до последней.
                currentPointNum++;
                if (isReturn && Vector3.Distance(transform.position,newPositions[newPositions.Length - 1]) < 0.3f) { //Если враг долетел и должен начать заново, то делаем текущей позицией стартовую точку
                        currentPointNum = 0;
                } 
        }
        if (Vector3.Distance(transform.position,newPositions[newPositions.Length - 1]) < 0.2 && !isReturn){ //Если враг долетел до последней точки, и он не должен вернуться, то убиваем его
            Destroy(gameObject);
        }

    }

    private Vector3[] NewPositionByPath(Transform[] pathPos){ //Даем массив из трансформов, создаем новый массив Вектор3 и в цикле каждому элементу Вектор3 присваиваем позицию трансформа.
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for (int i = 0; i < pathPoints.Length; i++){
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Wave.Smoothing(pathPositions);
        pathPositions = Wave.Smoothing(pathPositions);
        pathPositions = Wave.Smoothing(pathPositions);

        return pathPositions;
    }

    // Vector3[] Smoothing(Vector3[] path_positions){ //Метод для сглаживания пути
    //     Vector3[] newPathPositions = new Vector3[(path_positions.Length - 2) * 2 + 2];
    //     newPathPositions[0] = path_positions[0];
    //     newPathPositions[newPathPositions.Length - 1] = path_positions[path_positions.Length - 1];

    //     int j = 1;
    //     for (int i = 0; i < path_positions.Length - 2; i++){
    //         newPathPositions[j] = path_positions[i] + (path_positions[i + 1] - path_positions[i]) * 0.75f;
    //         newPathPositions[j + 1] = path_positions[i + 1] + (path_positions[i + 2] - path_positions[i + 1]) * 0.25f;
    //         j += 2;
    //     }
    //     return newPathPositions; 
    // }
}
