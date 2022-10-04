using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.ConstrainedExecution;

public class DataController : Singleton<DataController>
{

    private SaveData saveData = new SaveData();

    private string SAVE_PATH;
    private string SAVE_FILE = "/SaveFile.txt";

    private GameManager gameManager;
    private StageMapController stageMapController;
    private WeaponManager weaponManager;
    
    private void Start()
    {
        SAVE_PATH = Application.dataPath + "/Save/";

        if(!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadData();
    }

    public void SaveData()
    {
        gameManager = FindObjectOfType<GameManager>();
        stageMapController = FindObjectOfType<StageMapController>();
        weaponManager = FindObjectOfType<WeaponManager>();

        saveData.playerMoney = gameManager.gameMoney;
        saveData.playerGem = gameManager.gameGem;
        saveData.Pos = gameManager.characterPos;
        saveData.Rot = gameManager.characterRotation;
        //saveData.gameCount = gameManager.gameCount;

        saveData.mapTrigger = stageMapController.isTrigger;
        saveData.companionTrigger = stageMapController.isPlayerTrigger;
        saveData.questTrigger = stageMapController.questTrigger;

        saveData.weaponValue =     weaponManager.weaponValue;
        saveData.weaponStateNum =  weaponManager.WeaponStateNum;
        saveData.weaponMinDamage = weaponManager.minDamage;
        saveData.weaponMaxDamage = weaponManager.maxDamage;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_PATH + SAVE_FILE, json);


    }

    public void LoadData()
    {
        if(File.Exists(SAVE_PATH + SAVE_FILE))
        {
            string loadJason = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            saveData = JsonUtility.FromJson<SaveData>(loadJason);

            gameManager = FindObjectOfType<GameManager>();
            stageMapController = FindObjectOfType<StageMapController>();
            weaponManager = FindObjectOfType<WeaponManager>();

            gameManager.gameMoney = saveData.playerMoney;
            gameManager.gameGem = saveData.playerGem;
            gameManager.characterPos = saveData.Pos;
            gameManager.characterRotation = saveData.Rot;
            //gameManager.gameCount = saveData.gameCount;

            stageMapController.isTrigger = saveData.mapTrigger;
            stageMapController.isPlayerTrigger = saveData.companionTrigger;
            stageMapController.questTrigger = saveData.questTrigger;

            weaponManager.weaponValue = saveData.weaponValue;
            weaponManager.WeaponStateNum = saveData.weaponStateNum;
            weaponManager.minDamage = saveData.weaponMinDamage;
            weaponManager.maxDamage = saveData.weaponMaxDamage;
        }
        else
        {
            Debug.Log("세이브 없음");
        }
        
    }
}
