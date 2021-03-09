using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField]
    private int damageUpgradeCost = 100;
    [SerializeField]
    private int hpUpgradeCost = 80;
    [SerializeField]
    private int fireRateUpgradeCost = 150;
    [SerializeField]
    private float damageUpgradeDegree = 0.1f;
    [SerializeField]
    private float hpUpgradeDegree = 0.1f;
    [SerializeField]
    private float fireRateUpgradeDegree = 0.1f;

    private int totalScore;

    private void Start() {
        totalScore = Database.instance.LoadGameScore();
    }
    public void UpgradeDamage()
    {
        if (totalScore >= damageUpgradeCost)
        {
            totalScore -= damageUpgradeCost;
            Database.instance.SaveGameScore(false,totalScore);
            Database.instance.SaveDamageUpgrade(damageUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            Debug.Log("Damage upgraded succesfully");
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
            Database.instance.SaveHpUpgrade(hpUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            Debug.Log("Hp upgraded succesfully");
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
            Database.instance.SaveFireRateUpgrade(fireRateUpgradeDegree);
            MenuController.instance.RefreshScoreInMenu();
            Debug.Log("Fire rate upgraded succesfully");
        } 
        else {
            Debug.Log("Dont have enough money");
        }
    }

}
