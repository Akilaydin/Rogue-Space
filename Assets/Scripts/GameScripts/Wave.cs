using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Чтобы поля отображались в инспекторе
public class ShootingSettings {
    
    [Range(0,100)] //Делает в инспекторе ползунок
    public int  shotChance; //Шанс на выстрел
    public float shotTimeMin,shotTimeMax;
}

public class Wave : MonoBehaviour
{
    public ShootingSettings shootingSettings;
    [Space]
    public GameObject enemyObj;
    public int countInWave; //Количество врагов в волне
    public float enemySpeed;
    public float spawnTimeDelay; //Задержка появления врагов в волне
    public Transform[] pathPoints; //Путь, по которому будут двигаться волны
    public bool isReturn; //Уничтожается ли враг, после того как он пересек последнюю точку пути
    private GameObject newEnemy;

    //Test Wave
    [Header("TestWave")]
    public bool isTest;

    private PathFollowing followComponent;
    private Enemy enemyComponentScript;

    private void Start(){
        StartCoroutine(CreateEnemyWave());
    }

    public void SetEnemyActive(){
        if (newEnemy != null){
            newEnemy.SetActive(true);
        }
        
    }

    IEnumerator CreateEnemyWave(){
        for (int i = 0; i < countInWave; i++){
            newEnemy = Instantiate(enemyObj,enemyObj.transform.position,Quaternion.identity); //Создаем нового врага и даем на него ссылку
            followComponent = newEnemy.GetComponent<PathFollowing>(); //Находим у нового врага PathFollowing и передадим туда точки перемещения врага, скорость и переменную на возврат в начало пути
            followComponent.pathPoints = pathPoints;
            followComponent.enemySpeed = enemySpeed;
            followComponent.isReturn = isReturn;

            enemyComponentScript = newEnemy.GetComponent<Enemy>();
            enemyComponentScript.shotChance = shootingSettings.shotChance;
            enemyComponentScript.shotTimeMin = shootingSettings.shotTimeMin;
            enemyComponentScript.shotTimeMax = shootingSettings.shotTimeMax;

            Invoke("SetEnemyActive",0.7f);
            yield return new WaitForSeconds(spawnTimeDelay);

        }
        if (isTest){
            yield return new WaitForSeconds(5f); //Если тест, то создаем волну каждые 5 секунд
            StartCoroutine(CreateEnemyWave());
        }

        if (!isReturn){
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos(){ //Визуализация пути для удобного его редактирования
        NewPositionByPath(pathPoints);
    }

    void NewPositionByPath(Transform[] path){
        Vector3[] path_positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++){
            path_positions[i] = path[i].position;
        }
        path_positions = Smoothing(path_positions);
        path_positions = Smoothing(path_positions);
        //path_positions = Smoothing(path_positions);
        for (int i = 0; i < path_positions.Length - 1; i++){
            Gizmos.DrawLine(path_positions[i], path_positions[i + 1]);
        }
    }

    public static Vector3[] Smoothing(Vector3[] path_positions){ //Метод для сглаживания пути
        Vector3[] newPathPositions = new Vector3[(path_positions.Length - 2) * 2 + 2];
        newPathPositions[0] = path_positions[0];
        newPathPositions[newPathPositions.Length - 1] = path_positions[path_positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_positions.Length - 2; i++){
            newPathPositions[j] = path_positions[i] + (path_positions[i + 1] - path_positions[i]) * 0.75f;
            newPathPositions[j + 1] = path_positions[i + 1] + (path_positions[i + 2] - path_positions[i + 1]) * 0.25f;
            j += 2;
        }
        return newPathPositions; 
    }
}
