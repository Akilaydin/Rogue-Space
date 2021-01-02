using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    public GameObject[] bonusObjets;
    public float timeBonusSpawn;
    private List<GameObject> bonusesList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BonusCreation());
    }

    IEnumerator BonusCreation()
    {
        for (int i = 0; i < bonusObjets.Length; i++)
        {
            bonusesList.Add(bonusObjets[i]);
        }

        yield return new WaitForSeconds(7);

        while (true)
        {
            int randomIndex = Random.Range(0, bonusesList.Count);

            Instantiate(bonusesList[randomIndex], new Vector2(Random.Range(PlayerMovement.instance.borders.minX, PlayerMovement.instance.borders.maxX), PlayerMovement.instance.borders.maxY * 1.5f), Quaternion.identity);

            yield return new WaitForSeconds(timeBonusSpawn);
        }
    }
}
