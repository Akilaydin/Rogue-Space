using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Text scoreText, signInText;
    public static MenuController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        signInText = GameObject.Find("SignInText").GetComponent<Text>();
        RefreshScoreInMenu();
    }

    public void RefreshScoreInMenu()
    {
        scoreText.text = Database.instance.LoadGameScore().ToString();
    }


}
