using System.Collections;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    [HideInInspector] public Transform[] pathPoints; //Путь для перемещения врага, состоящий из точек.
    [HideInInspector] public float enemySpeed;

    [HideInInspector] public bool isReturn; //Отвечает за то, будет ли враг возвращаться на первую точку после того, как пересечет последнюю.
    [HideInInspector] public Vector3[] newPositions;


    private int currentPointNum;

    private void Start()
    {
        newPositions = NewPositionByPath(pathPoints);
        StartCoroutine(ReactivateEnemySprite());
        transform.position = newPositions[0]; //Отправляем в начало.
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPositions[currentPointNum], enemySpeed * Time.deltaTime); //Перемещаем врага

        if (Vector3.Distance(transform.position, newPositions[currentPointNum]) < 0.2f)
        { //Если враг долетел до следующей точки, но не до последней.
            currentPointNum++;
            if (isReturn && Vector3.Distance(transform.position, newPositions[newPositions.Length - 1]) < 0.3f)
            { //Если враг долетел и должен начать заново, то делаем текущей позицией стартовую точку
                currentPointNum = 0;
            }
        }

        if (Vector3.Distance(transform.position, newPositions[newPositions.Length - 1]) < 0.2 && !isReturn)
        { //Если враг долетел до последней точки, и он не должен вернуться, то убиваем его
            Destroy(gameObject);
        }
    }

    IEnumerator ReactivateEnemySprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private Vector3[] NewPositionByPath(Transform[] pathPos)
    { //Даем массив из трансформов, создаем новый массив Вектор3 и в цикле каждому элементу Вектор3 присваиваем позицию трансформа.
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Wave.Smoothing(pathPositions);
        pathPositions = Wave.Smoothing(pathPositions);
        pathPositions = Wave.Smoothing(pathPositions);

        return pathPositions;
    }
}
