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
    public float hpUpgrade;
    public float fireRateUpgrade;
    public int shipLevel; //Пока не используется

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
    }

    #region UpgradeSaves
    public void SaveDamageUpgrade(float damageUpgrade)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Upgrades.dat");
        UpgradesData upgrades = new UpgradesData();
        upgrades.damageUpgrade += damageUpgrade;
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveHpUpgrade(float hpUpgrade)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Upgrades.dat");
        UpgradesData upgrades = new UpgradesData();
        upgrades.hpUpgrade += hpUpgrade;
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveFireRateUpgrade(float fireRate)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Upgrades.dat");
        UpgradesData upgrades = new UpgradesData();
        upgrades.fireRateUpgrade += fireRate;
        bf.Serialize(file, upgrades);
        file.Close();
    }

    public void SaveShipLevelUpgrade(int shipLevel)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Upgrades.dat");
        UpgradesData upgrades = new UpgradesData();
        upgrades.shipLevel += shipLevel;
        bf.Serialize(file, upgrades);
        file.Close();
    }

    #endregion



    #region UpgradeLoads
    public float LoadDamageUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/Upgrades.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Upgrades.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.damageUpgrade;
        }
        else
            Debug.LogError("There is no save data for damage");
        return 0;
    }

    public float LoadHpUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/Upgrades.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Upgrades.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.hpUpgrade;
        }
        else
            Debug.LogError("There is no save data for hp");
        return 0;
    }

    public float LoadFireRateUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/Upgrades.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Upgrades.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.fireRateUpgrade;
        }
        else
            Debug.LogError("There is no save data for fire rate");
        return 0;
    }

    public float LoadShipLevelUpgrade()
    {
        if (File.Exists(Application.persistentDataPath + "/Upgrades.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Upgrades.dat", FileMode.Open);
            UpgradesData upgrades = (UpgradesData)bf.Deserialize(file);
            file.Close();
            return upgrades.shipLevel;
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
