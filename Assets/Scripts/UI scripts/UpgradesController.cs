using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradesController : MonoBehaviour
{


    [SerializeField]
    private int damageUpgradeCost = 100;
    [SerializeField]
    private float damageUpgradeCostIncremention = 1.6f;
    [SerializeField]
    private float damageUpgradeDegree = 0.1f;



    [SerializeField]
    private int hpUpgradeCost = 80;
    [SerializeField]
    private float hpUpgradeCostIncremention = 1.4f;
    [SerializeField]
    private float hpUpgradeDegree = 0.1f;



    [SerializeField]
    private int fireRateUpgradeCost = 150;
    [SerializeField]
    private float fireRateUpgradeDegree = 0.01f;
    [SerializeField]
    private float fireRateUpgradeCostIncremention = 2.5f;


    [SerializeField]
    private Text hpCostText,hpLevelText;
    [SerializeField]
    private Text damageCostText,damageLevelText;
    [SerializeField]
    private Text fireRateCostText,fireRateLevelText;

    private string upgradeLevelTemplate = "Уровень улучшения: ";
    private string upgradeCostTemplate = "Стоимость улучшения: ";

    private int totalScore;

    private void Start() {
        totalScore = Database.instance.LoadGameScore();

        UpgradeLevelOfDamage();
        UpgradeLevelOfFireRate();
        UpgradeLevelOfHP();
    }
    public void UpgradeDamage()
    {
        if (totalScore >= damageUpgradeCost)
        {
            totalScore -= damageUpgradeCost;
            Database.instance.SaveGameScore(false,totalScore);
            Database.instance.SaveDamageUpgrade(damageUpgradeDegree,damageUpgradeCostIncremention * damageUpgradeCost);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfDamage();
        } 
        else {
            Debug.Log("Dont have enough money");
        }
    }

    public void UpgradeHp()
    {
        if (totalScore >= hpUpgradeCost)
        {
            totalScore -= hpUpgradeCost;
            Database.instance.SaveGameScore(false,totalScore);
            Database.instance.SaveHpUpgrade(hpUpgradeDegree,hpUpgradeCostIncremention * hpUpgradeCost);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfHP();
        } 
        else {
            Debug.Log("Dont have enough money");
        }
    }

    public void UpgradeFireRate()
    {
        if (totalScore >= fireRateUpgradeCost)
        {
            totalScore -= fireRateUpgradeCost;
            Database.instance.SaveGameScore(false,totalScore);
            Database.instance.SaveFireRateUpgrade(fireRateUpgradeDegree,fireRateUpgradeCostIncremention * fireRateUpgradeCost);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfFireRate();
        } 
        else {
            Debug.Log("Dont have enough money");
        }
    }



    private void UpgradeLevelOfHP()
    {
        hpLevelText.text = upgradeLevelTemplate + Database.instance.LoadHpUpgradeLevel().ToString();
        hpCostText.text = upgradeCostTemplate + Database.instance.LoadHpUpgradeCost().ToString();
    }

    private void UpgradeLevelOfDamage()
    {
        damageLevelText.text = upgradeLevelTemplate + Database.instance.LoadDamageUpgradeLevel().ToString();
        damageCostText.text = upgradeCostTemplate + Database.instance.LoadDamageUpgradeCost().ToString();
    }

    private void UpgradeLevelOfFireRate()
    {
        fireRateLevelText.text = upgradeLevelTemplate + Database.instance.LoadFireRateUpgradeLevel().ToString();
        fireRateCostText.text = upgradeCostTemplate + Database.instance.LoadFireRateUpgradeCost().ToString();
    }
}
