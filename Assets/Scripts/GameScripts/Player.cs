using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class Player : MonoBehaviour
{
    [Header("Player")]
    public static Player instance = null; //Ссылка на игрока, чтобы можно было взаимодействовать с ним из других скриптов.
    public int playerHealth = 1;
    public int maxHealth = 5;
    public float invincibilityDuration = 1f;
    private float invincibilityDeltaTime;
    public bool isInvincible = false;
    public bool isCleaningBullets = false;
    public ParticleSystem playerCleanAfterInvincibility;
    private SpriteRenderer playerRenderer;

    [Header("Tricks for testing")]
    [SerializeField]
    private float smallSizeMultiplier;
    [SerializeField]
    private int timeScaleBoost;
    [SerializeField]
    private GameObject bombToClearTheScreen;

    [Header("UI")]
    [SerializeField]
    private GameObject addPanel;
    private Slider hpSliderPlayer;
    private Slider shieldSliderPlayer;
    




    


    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        


    }

    void Start(){
        hpSliderPlayer = GameObject.FindGameObjectWithTag("HP_UI").GetComponent<Slider>();
        shieldSliderPlayer = GameObject.FindGameObjectWithTag("Shield_UI").GetComponent<Slider>();
        shieldSliderPlayer.fillRect.gameObject.SetActive(false); 
        invincibilityDeltaTime = invincibilityDuration / 7; //Getting 5 frames of invincibility.
        playerRenderer = GetComponent<SpriteRenderer>();
        RefreshHpBar(); 

    }

    private void Update(){
        if (Input.GetKey(KeyCode.LeftShift)){     /////////////DEBUG
            transform.localScale = new Vector3(1 * smallSizeMultiplier, 1* smallSizeMultiplier, 1 * smallSizeMultiplier);
            gameObject.GetComponent<PlayerShooting>().enabled = false;
        } else if (!Input.GetKey(KeyCode.LeftShift)){ ///////////////DEBUG
            transform.localScale = Vector3.one;
            gameObject.GetComponent<PlayerShooting>().enabled = true;
        }                                       

        if (Input.GetKeyDown(KeyCode.LeftAlt)){ ///////////////DEBUG
            Time.timeScale = timeScaleBoost;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)){ ///////////////DEBUG
            Time.timeScale = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }                                       

        if (Input.GetKeyDown(KeyCode.B)){
            Instantiate(bombToClearTheScreen,transform.position,Quaternion.identity); ///////////////DEBUG
        }     
    }

    public void GetDamage(int damage){
        if (!isInvincible){
            Instantiate(playerCleanAfterInvincibility,transform.position,Quaternion.identity);
            playerHealth -= damage;
            StartCoroutine(BecomeInvincible());
            RefreshHpBar();
        }
        
        
    }
    
    public void RefreshHpBar(){
        hpSliderPlayer.value = (float) playerHealth / maxHealth; //Чтобы нормально работало с ползунком, нужно делить на десять.
    }

    public void Destruction(){
        Destroy(gameObject);
        addPanel.SetActive(false); 
        
    }
    
    private IEnumerator BecomeInvincible(){
            isInvincible = true;
            for (float i = 0; i < invincibilityDuration; i+=invincibilityDeltaTime){
                if (playerRenderer.color.a == 1)
                {
                    playerRenderer.color = new Color(playerRenderer.color.r,playerRenderer.color.g,playerRenderer.color.b, 0.5f); //Выключаем
                } 
                else 
                {
                    playerRenderer.color = new Color(playerRenderer.color.r,playerRenderer.color.g,playerRenderer.color.b, 1); //Включаем
                }
                
                yield return new WaitForSeconds(invincibilityDeltaTime);
            }
            playerRenderer.color = new Color(playerRenderer.color.r,playerRenderer.color.g,playerRenderer.color.b, 1);

            if (isCleaningBullets){
                GetComponentInChildren<CircleCollider2D>().enabled = true;
                yield return new WaitForSeconds(0.05f);
                GetComponentInChildren<CircleCollider2D>().enabled = false;
            }
            isInvincible = false;

    }

   
}
