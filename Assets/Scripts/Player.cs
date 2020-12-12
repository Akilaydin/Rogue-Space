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
    
    [Header("Tricks for testing")]
    public float smallSizeMultiplier;
    public int timeScaleBoost;

    [Header("UI")]
    private Slider hpSliderPlayer;
    private Slider shieldSliderPlayer;
    public GameObject addPanel;




    private void Update(){
        if (Input.GetKey(KeyCode.LeftShift)){ /////////////DEBUG
            transform.localScale = new Vector3(1 * smallSizeMultiplier, 1* smallSizeMultiplier, 1 * smallSizeMultiplier);
            gameObject.GetComponent<PlayerShooting>().enabled = false;
        } else if (!Input.GetKey(KeyCode.LeftShift)){
            transform.localScale = Vector3.one;
            gameObject.GetComponent<PlayerShooting>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt)){ ///////////////DEBUG
            Time.timeScale = timeScaleBoost;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            Time.timeScale = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }


    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        hpSliderPlayer = GameObject.FindGameObjectWithTag("HP_UI").GetComponent<Slider>();
        shieldSliderPlayer = GameObject.FindGameObjectWithTag("Shield_UI").GetComponent<Slider>();

    }

    void Start(){

        
        shieldSliderPlayer.fillRect.gameObject.SetActive(false); 
        RefreshHpBar(); 

    }

    public void GetDamage(int damage){
        playerHealth -= damage;
        RefreshHpBar();
    }
    
    public void RefreshHpBar(){
        hpSliderPlayer.value = (float) playerHealth / maxHealth; //Чтобы нормально работало с ползунком, нужно делить на десять.
    }
    public void Destruction(){
        Destroy(gameObject);
        addPanel.SetActive(false); 
        
    }
    
   
}
