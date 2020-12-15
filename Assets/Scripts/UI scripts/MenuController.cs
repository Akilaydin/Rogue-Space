using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        RefreshScore();
    }

    private void RefreshScore(){
        scoreText.text = Database.instance.LoadGameScore().ToString();
    }
}
