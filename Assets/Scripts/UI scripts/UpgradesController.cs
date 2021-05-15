using UnityEngine;
using UnityEngine.UI;

public class UpgradesController : MonoBehaviour
{

    [SerializeField]
    private float damageUpgradeCostIncremention = 2.2f;
    [SerializeField]
    private float damageUpgradeDegree = 0.1f;


    [SerializeField]
    private float hpUpgradeCostIncremention = 2.2f;
    [SerializeField]
    private float hpUpgradeDegree = 0.1f;

    [SerializeField]
    private float fireRateUpgradeCostIncremention = 4f;
    [SerializeField]
    private float fireRateUpgradeDegree = 0.005f;



    [SerializeField]
    private Text hpCostText, hpLevelText, hpCurrentText;
    [SerializeField]
    private Text damageCostText, damageLevelText, damageCurrentText;
    [SerializeField]
    private Text fireRateCostText, fireRateLevelText, fireRateCurrentText;


    private string upgradeLevelTemplate = "Уровень:";
    private string upgradeCostTemplate = "Стоимость:";
    private string currentUpgradeTemplate = "Текущий:";

    private int totalScore;

    private void Start()
    {
        totalScore = Database.instance.LoadGameScore();
        UpgradeLevelOfDamage();
        UpgradeLevelOfFireRate();
        UpgradeLevelOfHP();
    }

    public void UpgradeDamage()
    {
        int damageUpgradeLocalCost;
        damageUpgradeLocalCost = Database.instance.LoadDamageUpgradeCost();

        if (totalScore >= damageUpgradeLocalCost)
        {
            totalScore -= damageUpgradeLocalCost;
            Database.instance.SaveGameScore(false, totalScore);
            Database.instance.SaveDamageCost((int)(damageUpgradeLocalCost * damageUpgradeCostIncremention));
            Database.instance.SaveAndIncreaseDamageLevel();
            Database.instance.SaveCurrentDamage(damageUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfDamage();
        }
        else
        {
            Debug.Log("Dont have enough money");
        }
    }

    public void UpgradeHp()
    {
        int hpUpgradeLocalCost;
        hpUpgradeLocalCost = Database.instance.LoadHpUpgradeCost();

        if (totalScore >= hpUpgradeLocalCost)
        {
            totalScore -= hpUpgradeLocalCost;
            Database.instance.SaveGameScore(false, totalScore);
            Database.instance.SaveHPCost((int)(hpUpgradeLocalCost * hpUpgradeCostIncremention));
            Database.instance.SaveAndIncreaseHPLevel();
            Database.instance.SaveCurrentHP(hpUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfHP();
        }
        else
        {
            Debug.Log("Dont have enough money");
        }
    }

    public void UpgradeFireRate()
    {
        int fireRateUpgradeLocalCost;
        fireRateUpgradeLocalCost = Database.instance.LoadFireRateUpgradeCost();
        if (totalScore >= fireRateUpgradeLocalCost)
        {
            totalScore -= fireRateUpgradeLocalCost;
            Database.instance.SaveGameScore(false, totalScore);
            Database.instance.SaveFireRateCost((int)(fireRateUpgradeLocalCost * fireRateUpgradeCostIncremention));
            Database.instance.SaveAndIncreaseFireRateLevel();
            Database.instance.SaveCurrentFireRate(fireRateUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            UpgradeLevelOfFireRate();

        }
        else
        {
            Debug.Log("Dont have enough money");
        }
    }



    private void UpgradeLevelOfHP()
    {
        hpCurrentText.text = currentUpgradeTemplate + Database.instance.LoadCurrentHP().ToString();
        hpCostText.text = upgradeCostTemplate + Database.instance.LoadHpUpgradeCost().ToString();
        hpLevelText.text = upgradeLevelTemplate + Database.instance.LoadHpUpgradeLevel().ToString();
    }

    private void UpgradeLevelOfDamage()
    {
        damageCurrentText.text = currentUpgradeTemplate + Database.instance.LoadCurrentDamage().ToString();
        damageCostText.text = upgradeCostTemplate + Database.instance.LoadDamageUpgradeCost().ToString();
        damageLevelText.text = upgradeLevelTemplate + Database.instance.LoadDamageUpgradeLevel().ToString();
    }

    private void UpgradeLevelOfFireRate()
    {
        fireRateCurrentText.text = currentUpgradeTemplate + Database.instance.LoadCurrentFireRate().ToString();
        fireRateCostText.text = upgradeCostTemplate + Database.instance.LoadFireRateUpgradeCost().ToString();
        fireRateLevelText.text = upgradeLevelTemplate + Database.instance.LoadFireRateUpgradeLevel().ToString();
    }
}
