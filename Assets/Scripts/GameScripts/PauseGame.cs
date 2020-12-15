using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Pause");
        LevelController.instance.PauseGame();
    }
}
