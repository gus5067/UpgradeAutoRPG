using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int playerMoney;
    public int playerGem;


    public int weaponValue;
    public int weaponStateNum;
    public int weaponMinDamage;
    public int weaponMaxDamage;


    public Vector3 Pos;
    public Vector3 Rot;

    //public Dictionary<string, int> gameCount = new Dictionary<string, int>();

    public bool[] mapTrigger = new bool[4];
    public bool[] companionTrigger = new bool[2];
    public bool[] questTrigger = new bool[2];

}
