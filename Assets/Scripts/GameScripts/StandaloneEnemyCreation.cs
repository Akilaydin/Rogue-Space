using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneEnemyCreation : MonoBehaviour
{
    public GameObject[] enemyObjects;
    public float timeEnemySpawn;
    private List<GameObject> enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BonusCreation());
    }

    IEnumerator BonusCreation()
    {
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyList.Add(enemyObjects[i]);
        }

        yield return new WaitForSeconds(7);

        while (true)
        {
            int randomIndex = Random.Range(0, enemyList.Count);

            Instantiate(enemyList[randomIndex], new Vector2(Random.Range(PlayerMovement.instance.borders.minX, PlayerMovement.instance.borders.maxX), PlayerMovement.instance.borders.maxY * 1.5f), Quaternion.identity);

            yield return new WaitForSeconds(timeEnemySpawn);
        }
    }
}
