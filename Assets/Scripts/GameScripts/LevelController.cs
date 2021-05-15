using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class EnemyWaves
{
    // The Enemy Wave prefab to be spawned.
    public GameObject enemyWave;
    public GameObject shootingBossWave;
    public GameObject bossWave;
}



public class LevelController : MonoBehaviour, IUnityAdsListener
{
    public static LevelController instance;
    public EnemyWaves[] enemyWaves;
    public EnemyWaves bossWave;

    [Header("Wave settings")]

    [SerializeField]
    private int countToSpawnNewWave = 2;
    [SerializeField]
    private float delayBetweenWaves = 3;
    [SerializeField]
    private float delayBetweenShootingBossWaves = 10;
    private int enemyWaveIndex = 0;
    public bool isBossFight = false;

    [Header("PauseSettings")]
    public GameObject pausePanel;
    public GameObject loadingPanel;
    public GameObject addPanel;
    public GameObject settingsPanel;
    public GameObject pauseButton;
    public Slider loadingSlider;
    private bool isPaused;

    [Header("Other Stuff")]
    public GameObject clearScreenBomb;
    private bool isRewardedAddGlobal = false;
    private Text scoreText;
    public int totalScore;
    private bool isPausedForSavingScore = false;



    private void Awake()
    {
        Time.timeScale = 1;
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
        if (Database.instance.LoadGameScore() > 0)
        {
            totalScore = Database.instance.LoadGameScore();
        }
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        LanguageScore();
        CheckAdds();
        StartCoroutine(CreateEnemyWaves());
        StartCoroutine(CreateShootingBossWaves());
    }

    void Update()
    {
        if (Player.instance.playerHealth <= 0 && isPaused == false)
        {
            addPanel.SetActive(true);
            Time.timeScale = 0; //Not PauseGame() cause I dont want to double pause menu.
        }
    }

    IEnumerator CreateEnemyWaves()
    {
        while (true)
        {

            CheckIfEnemyIndexIsTooBig();
            CheckIsBossFight();
            if (Player.instance != null && AreThereEnemiesAlive() == false && isBossFight == false)
            {
                CheckIfEnemyIndexIsTooBig();
                Instantiate(enemyWaves[enemyWaveIndex].enemyWave);
                enemyWaveIndex++;
                yield return new WaitForSeconds(delayBetweenWaves);
            }

            if (enemyWaveIndex > enemyWaves.Length - 1 && isBossFight == false)
            {
                CheckIfEnemyIndexIsTooBig();
                Instantiate(bossWave.bossWave);
                isBossFight = true;
            }
            yield return new WaitForSeconds(1);
        }

    }
    private void CheckIfEnemyIndexIsTooBig()
    {
        if (enemyWaveIndex >= enemyWaves.Length)
        {
            Debug.Log("Im in if enemy>index" + "Index is " + enemyWaveIndex + "And length is " + enemyWaves.Length);
            enemyWaveIndex = Random.Range(0, enemyWaves.Length - 1);
            Debug.Log("After random index becomes " + enemyWaveIndex);
        }
    }
    private void CheckIsBossFight()
    {
        if (GameObject.FindObjectsOfType<BossScript>().Length > 0)
        {
            isBossFight = true;
        }
        else
        {
            isBossFight = false;
        }
    }
    private bool AreThereEnemiesAlive()
    { //Returns true if there are more than 1 enemy alive. Returns false in the opposite variant.
        if (GameObject.FindObjectsOfType<Enemy>().Length <= countToSpawnNewWave)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator CreateShootingBossWaves()
    {
        while (true)
        {
            CheckIsBossFight();
            if (Player.instance != null && isBossFight == false)
            {
                Instantiate(bossWave.shootingBossWave);
                yield return new WaitForSeconds(delayBetweenShootingBossWaves);
            }
            else
            {
                yield return new WaitForSeconds(2);
            }

        }
    }



    #region SavingScoreAfterQuitting
    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }
    void OnApplicationPause(bool pauseStatus)
    {
        isPausedForSavingScore = pauseStatus;
        Database.instance.SaveGameScore(true, 0);
    }
    void OnApplicationQuit()
    {
        Database.instance.SaveGameScore(true, 0);
        StopAllCoroutines();

    }
    #endregion

    #region  Score,Pause, menu logic 
    public void ScoreInGame(int score)
    {
        totalScore += score;
        LanguageScore();

        Database.instance.SaveGameScore(true, 0);
    }
    private void LanguageScore()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            scoreText.text = "Очки: " + totalScore;
        }
        else
        {
            scoreText.text = "Score: " + totalScore;
        }
    }

    public void PauseGame()
    {
        if (isPaused == false)
        {
            isPaused = true;
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            pauseButton.SetActive(true);
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void GoToSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void GoToMainMenu()
    {
        Player.instance.Destruction();
        Time.timeScale = 0;
        StartCoroutine(loadLevelAsync(0));
        loadingPanel.SetActive(true);
        pauseButton.SetActive(false);

    }

    #endregion

    #region AddsLogic
    private void CheckAdds()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize("3913797", false);

        }
        else if (!Advertisement.isSupported)
        {
            Debug.Log("Advertisment isn't supported somehow");
        }
    }
    public void ShowAdds(bool isRewarded)
    {
        if (Advertisement.IsReady("video") && !isRewarded)
        {
            if (Random.value <= 0.1)
            { //Смотреть рекламу в одном из десяти случаев после смерти.
                isRewardedAddGlobal = false;
                Advertisement.Show("video");
            }
            else
            {
                GoToMainMenu();
            }
        }
        else if (Advertisement.IsReady("rewardedVideo") && isRewarded)
        {
            isRewardedAddGlobal = true;
            Advertisement.Show("rewardedVideo");



        }
        else
        {
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

        if (showResult == ShowResult.Finished && isRewardedAddGlobal == true)
        {
            Debug.Log("The REWARDED add has been sucessfully watched");

            addPanel.SetActive(false);
            Player.instance.playerHealth = Player.instance.playerMaxHealth;
            Player.instance.RefreshHpBar();
            Time.timeScale = 1;
            Instantiate(clearScreenBomb, Player.instance.transform.position, Quaternion.identity);

        }
        else if (showResult == ShowResult.Finished && isRewardedAddGlobal == false)
        {
            Debug.Log("The NOT REWARDED add has been sucessfully watched");
            GoToMainMenu();
            Player.instance.Destruction();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("The add has been skipped");
        }
        else
        {
            Debug.Log("Some error in AdsDidFinish. Maybe the add has been interrupted or something.");
        }
    }
    IEnumerator loadLevelAsync(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (loadOperation.isDone == false)
        {
            float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = loadProgress;
            yield return null;
        }
    }
    public void OnUnityAdsDidError(string message)
    {

    }
    #endregion
}
