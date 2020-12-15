using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class EnemyWaves {
     // Time when the wave will appear on the stage
    public float timeToStart;
    // The Enemy Wave prefab to be spawned.
    public GameObject wave;
    // This wave after which the game will end.
    public bool is_Last_Wave;

}

public class LevelController : MonoBehaviour, IUnityAdsListener
{
    // Static reference to the LevelController (can be used in other scripts).
    public static LevelController instance;
    //Array of player ships
    public GameObject[] playerShip;
    // Reference to the EnemyWaves.
    public EnemyWaves[] enemyWaves;
    private bool isFinal = false; 

    public GameObject pausePanel;
    public GameObject addPanel;
    private bool isPaused;
    public GameObject[] pauseButtons; //0 элемент - Возобновить, 1 элемент - Настройки, 2 элемент - Выход.

    [Header("Other Stuff")]
    public GameObject clearScreenBomb;
    private bool isRewardedAddGlobal = false;
    private Text scoreText;
    private int totalScore; //Временно здесь, потом перенести в БД.


    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        Debug.Log(scoreText.text);
        if (Advertisement.isSupported)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize("3913797",false);

        } 
        else if (!Advertisement.isSupported)
        {
            Debug.Log("Advertisment isn't supported somehow");
        }
        // Create all enemy waves...
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            // Start CreateEnemyWave as a coroutine.
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }

    void Update(){
        if (Player.instance.playerHealth <= 0 && isPaused == false){
            addPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (isFinal && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isPaused == false){
            Debug.Log("Win");
            PauseGame();
        }

    }

    public void ScoreInGame(int score){
        totalScore += score;
        scoreText.text = "Очки: "+ totalScore;
    }
    public void PauseGame(){
        if (isPaused == false){
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        } else {
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void GoToSettings(){
        Debug.Log("Выход в настройки");
    }
    public void GoToMainMenu(){
        Player.instance.Destruction();
        SceneManager.LoadScene(0);
        
    }
    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool final)
    {
        // if creation wave time ! = 0 and player is alive ...create a wave
        if (delay != 0){
            yield return new WaitForSeconds(delay);
        }
        if (Player.instance != null){
            Instantiate(Wave);
        }
        if (final == true){
            isFinal = true;
        }
    }



    public void ShowAdds(bool isRewarded){
        if (Advertisement.IsReady("video") && !isRewarded){
            if (Random.value <= 0.1 ){ //Смотреть рекламу в одном из десяти случаев после смерти.
                isRewardedAddGlobal = false;
                Advertisement.Show("video");
            } else {
                GoToMainMenu();
            }    
        }
        else if (Advertisement.IsReady("rewardedVideo") && isRewarded){
            isRewardedAddGlobal = true;
            Advertisement.Show("rewardedVideo");
            
            

        } else {
            GoToMainMenu();
            Debug.Log("Else branch in showAdds method in player script. Add is not working or isn't ready maybe");
            
        }
    }


    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
        if (showResult == ShowResult.Finished && isRewardedAddGlobal == true){
            Debug.Log("The REWARDED add has been sucessfully watched");
            
            addPanel.SetActive(false);
            Player.instance.playerHealth = Player.instance.maxHealth;
            Player.instance.RefreshHpBar();
            Time.timeScale = 1;
            Instantiate(clearScreenBomb,Player.instance.transform.position,Quaternion.identity);

        } 
        else if (showResult == ShowResult.Finished && isRewardedAddGlobal == false){
            Debug.Log("The NOT REWARDED add has been sucessfully watched");
            GoToMainMenu();
            Player.instance.Destruction();
        }
        else if (showResult == ShowResult.Skipped){
            Debug.Log("The add has been skipped");
        } 
        else {
            Debug.Log("Some error in AdsDidFinish. Maybe the add has been interrupted or something.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }
}
