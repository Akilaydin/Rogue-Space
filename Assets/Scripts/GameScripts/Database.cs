using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



[System.Serializable]
public class DataSave
{
    public int savedScore;
}
[System.Serializable]
public class Upgrades //Уровень улучшения параметра и стоимость улучшения параметра.
{
    public int damageUpgradeCost;
    public int damageUpgradeLevel;
    public float currentDamage;

    public int fireRateUpgradeCost;
    public int fireRateUpgradeLevel;
    public float currentFireRate;

    public int hpUpgradeCost;
    public int hpUpgradeLevel;
    public float currentHp;

    public int shipUpgradeCost;
    public int shipUpgradeLevel;
    public int currentShip;

    public int shieldUpgradeCost;
    public int shieldUpgradeLevel;
    public float currentShieldUpgrade;
}

public class Database : MonoBehaviour
{
    private const string damageCostSavesPath = "/UpgradeDamageCost.dat";
    private const string fireRateCostSavesPath = "/UpgradeFireRateCost.dat";
    private const string hpCostSavesPath = "/UpgradeHPCost.dat";

    private const string damageLevelSavesPath = "/UpgradeDamageLevel.dat";
    private const string fireRateLevelSavesPath = "/UpgradeFireRateLevel.dat";
    private const string hpLevelSavesPath = "/UpgradeHPLevel.dat";

    private const string currentDamageSavesPath = "/UpgradeCurrentDamage.dat";
    private const string currentFireRateSavesPath = "/UpgradeCurrentFireRate.dat";
    private const string currentHPSavesPath = "/UpgradeCurrentHP.dat";

    public static Database instance;
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

        DontDestroyOnLoad(this.gameObject);
        Debug.Log(Application.persistentDataPath);
    }

    #region UpgradeCostSaves
    public void SaveDamageCost(int damageUpgradeCost)
    {
        Upgrades upgrades = new Upgrades();
        upgrades.damageUpgradeCost = damageUpgradeCost;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + damageCostSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveFireRateCost(int fireRateUpgradeCost)
    {
        Upgrades upgrades = new Upgrades();
        upgrades.fireRateUpgradeCost = fireRateUpgradeCost;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fireRateCostSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveHPCost(int hpUpgradeCost)
    {
        Upgrades upgrades = new Upgrades();
        upgrades.hpUpgradeCost = hpUpgradeCost;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + hpCostSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }
    #endregion

    #region UpgradeLevelSaves
    public void SaveAndIncreaseDamageLevel()
    {
        int currentLevel = LoadDamageUpgradeLevel();
        Upgrades upgrades = new Upgrades();
        upgrades.damageUpgradeLevel = currentLevel + 1;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + damageLevelSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveAndIncreaseFireRateLevel()
    {
        int currentLevel = LoadFireRateUpgradeLevel();
        Upgrades upgrades = new Upgrades();
        upgrades.fireRateUpgradeLevel = currentLevel + 1;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fireRateLevelSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }
    public void SaveAndIncreaseHPLevel()
    {
        int currentLevel = LoadHpUpgradeLevel();
        Upgrades upgrades = new Upgrades();
        upgrades.hpUpgradeLevel = currentLevel + 1;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + hpLevelSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }
    #endregion

    #region CurrentUpgradesSaves
    public void SaveCurrentDamage(float damageIncrement)
    {
        float newDamage = LoadCurrentDamage() + damageIncrement;
        Upgrades upgrades = new Upgrades();
        upgrades.currentDamage = newDamage;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + currentDamageSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveCurrentFireRate(float fireRateDecrement)
    {
        float newFireRate = LoadCurrentFireRate() - fireRateDecrement;
        Debug.Log(newFireRate);
        Debug.Log(LoadCurrentFireRate());
        Debug.Log(fireRateDecrement);
        Upgrades upgrades = new Upgrades();
        upgrades.currentFireRate = newFireRate;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + currentFireRateSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveCurrentHP(float hpIncrement)
    {
        float newHP = LoadCurrentHP() + hpIncrement;
        Upgrades upgrades = new Upgrades();
        upgrades.currentHp = newHP;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + currentHPSavesPath);
        bf.Serialize(file, upgrades);
        file.Close();
    }
    #endregion

    #region UpgradeLoads
    public float LoadCurrentDamage()
    {
        float defaultDamage = 1;
        if (File.Exists(Application.persistentDataPath + currentDamageSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + currentDamageSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.currentDamage;
        }
        else
        return defaultDamage;
    }

    public float LoadCurrentFireRate()
    {
        float defaultFireRate = 0.3f;
        if (File.Exists(Application.persistentDataPath + currentFireRateSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + currentFireRateSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.currentFireRate;
        }
        else
        return defaultFireRate;
    }

    public float LoadCurrentHP()
    {
        float defaultHP = 5;
        if (File.Exists(Application.persistentDataPath + currentHPSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + currentHPSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.currentHp;
        }
        else
        return defaultHP;
    }


    public int LoadDamageUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + damageCostSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + damageCostSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgradeCost;
        }
        else
        return 100;
    }

    public int LoadDamageUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + damageLevelSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + damageLevelSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgradeLevel;
        }
        else
        return 1;
    }
    public int LoadHpUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + hpCostSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + hpCostSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgradeCost;
        }
        else
        return 80;
    }
    public int LoadHpUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + hpLevelSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + hpLevelSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgradeLevel;
        }
        else
        return 1;
    }

    public int LoadFireRateUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + fireRateCostSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fireRateCostSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgradeCost;
        }
        else
        return 1000;
    }

    public int LoadFireRateUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + fireRateLevelSavesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fireRateLevelSavesPath, FileMode.Open);
            Upgrades upgrades = (Upgrades)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgradeLevel;
        }
        else
        return 1;
    }



    //public int LoadShipUpgradeLevel()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/UpgradesShipLevel.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/UpgradesShipLevel.dat", FileMode.Open);
    //        Upgrades upgrades = (Upgrades)bf.Deserialize(file);
    //        file.Close();
    //        return upgrades.shipUpgradeLevel;
    //    }
    //    else
    //        Debug.LogError("There is no save data for ship level");
    //    return 1;
    //}

    // public int LoadShipLevelUpgradeCost()
    // {
    //     if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
    //     {
    //         BinaryFormatter bf = new BinaryFormatter();
    //         FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
    //         UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
    //         file.Close();
    //         return upgrades.fireRateUpgradeCost; //Change fireRate
    //     }
    //     else
    //         Debug.LogError("There is no save data for ship level");
    //     return 0;
    // }
    #endregion


    #region SaveCheckers
    //public bool IfCurrentDamageSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + currentDamageSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfDamageCostSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + damageCostSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfDamageLevelSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + damageLevelSavesPath))
    //        return true;

    //    return false;
    //}



    //public bool IfCurrentHPSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + currentHPSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfHPCostSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + hpCostSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfHPLevelSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + hpLevelSavesPath))
    //        return true;

    //    return false;
    //}



    //public bool IfCurrentFireRateSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + currentFireRateSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfFireRateCostSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + fireRateCostSavesPath))
    //        return true;

    //    return false;
    //}
    //public bool IfFireRateLevelSaveExists()
    //{
    //    if (File.Exists(Application.persistentDataPath + fireRateLevelSavesPath))
    //        return true;

    //    return false;
    //}
    #endregion



    public void SaveGameScore(bool isInGame, int totalScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        DataSave data = new DataSave();
        if (isInGame == true)
        {
            data.savedScore = LevelController.instance.totalScore;
        }
        else
        {
            data.savedScore = totalScore;
        }

        bf.Serialize(file, data);
        file.Close();
    }

    public int LoadGameScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            DataSave data = (DataSave)bf.Deserialize(file);
            file.Close();
            return data.savedScore;
        }
        else
            Debug.LogError("There is no save data!");
        return 0;

    }
}
