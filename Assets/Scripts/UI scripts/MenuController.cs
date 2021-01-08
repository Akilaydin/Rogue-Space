using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Text scoreText, signInText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        signInText = GameObject.Find("SignInText").GetComponent<Text>();
        RefreshScore();
    }

    private void RefreshScore(){
        scoreText.text = Database.instance.LoadGameScore().ToString();
    }

    
}
