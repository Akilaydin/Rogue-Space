using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



[System.Serializable]
public class DataSave
{
    public int savedScore;
}
[System.Serializable]
public class UpgradesData //Показывает не сами параметры, а степень их улучшения.
{
    public float damageUpgrade;
    public int damageUpgradeLevel;
    public int damageUpgradeCost; 


    public float hpUpgrade;
    public int hpUpgradeLevel;
    public int hpUpgradeCost; 


    public float fireRateUpgrade;
    public int fireRateUpgradeLevel;
    public int fireRateUpgradeCost; 


    public int shipLevel; //Пока не используется
    public int shipLevelUpgradeLevel;
    

}
public class Database : MonoBehaviour
{
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

    #region UpgradeSaves
    public void SaveDamageUpgrade(float damageUpgrade, float damageUpgradeCostIncremention)
    {
        UpgradesData upgrades = new UpgradesData();
        upgrades.damageUpgrade = LoadDamageUpgrade() + damageUpgrade;
        upgrades.damageUpgradeLevel = LoadDamageUpgradeLevel() + 1;
        upgrades.damageUpgradeCost = (int) (LoadDamageUpgradeCost() * damageUpgradeCostIncremention);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/UpgradesDamage.dat");
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveHpUpgrade(float hpUpgrade, float hpUpgradeCostIncremention)
    {
        UpgradesData upgrades = new UpgradesData();
        upgrades.hpUpgrade = LoadHpUpgrade() + hpUpgrade;
        upgrades.hpUpgradeLevel = LoadHpUpgradeLevel() + 1;
        upgrades.hpUpgradeCost = (int) (LoadFireRateUpgradeCost() * hpUpgradeCostIncremention);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/UpgradesHp.dat");
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveFireRateUpgrade(float fireRateUpgrade, float fireRateUpgradeCostIncremention)
    {
        UpgradesData upgrades = new UpgradesData();
        upgrades.fireRateUpgrade += LoadFireRateUpgrade() + fireRateUpgrade;
        upgrades.fireRateUpgradeLevel = LoadFireRateUpgradeLevel() + 1;
        upgrades.fireRateUpgradeCost = (int) (LoadFireRateUpgradeCost() * fireRateUpgradeCostIncremention);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/UpgradesFireRate.dat");
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveShipLevelUpgrade(int shipLevel)
    {
        UpgradesData upgrades = new UpgradesData();
        upgrades.shipLevel = LoadShipLevelUpgrade() + shipLevel;
        upgrades.shipLevelUpgradeLevel = LoadShipLevelUpgradeLevel() + 1;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/UpgradesShipLevel.dat");
        bf.Serialize(file, upgrades);
        file.Close();
    }

    #endregion



    #region UpgradeLoads
    public float LoadDamageUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgrade;
        }
        else
            Debug.LogError("There is no save data for damage");
        return 0;
    }

    public int LoadDamageUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgradeLevel;
        }
        else
            Debug.LogError("There is no save data for damage");
        return 0;
    }

    public int LoadDamageUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgradeCost;
        }
        else
            Debug.LogError("There is no save data for damage");
        return 0;
    }

    public float LoadHpUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesHp.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesHp.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgrade;
        }
        else
            Debug.LogError("There is no save data for hp");
        return 0;
    }

    public int LoadHpUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesHp.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesHp.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgradeLevel;
        }
        else
            Debug.LogError("There is no save data for hp");
        return 0;
    }

    public int LoadHpUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgradeCost;
        }
        else
            Debug.LogError("There is no save data for hp");
        return 0;
    }

    public float LoadFireRateUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesFireRate.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesFireRate.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgrade;
        }
        else
            Debug.LogError("There is no save data for fire rate");
        return 0;
    }

    public int LoadFireRateUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesFireRate.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesFireRate.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgradeLevel;
        }
        else
            Debug.LogError("There is no save data for fire rate");
        return 0;
    }

    public int LoadFireRateUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgradeCost;
        }
        else
            Debug.LogError("There is no save data for fire rate");
        return 0;
    }

    public int LoadShipLevelUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesShipLevel.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesShipLevel.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.shipLevel;
        }
        else
            Debug.LogError("There is no save data for ship level");
        return 0;
    }

    public int LoadShipLevelUpgradeLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesShipLevel.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesShipLevel.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.shipLevelUpgradeLevel;
        }
        else
            Debug.LogError("There is no save data for ship level");
        return 0;
    }

    public int LoadShipLevelUpgradeCost()
    {
        if (File.Exists(Application.persistentDataPath + "/UpgradesDamage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/UpgradesDamage.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgradeCost;
        }
        else
            Debug.LogError("There is no save data for ship level");
        return 0;
    }
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
