using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReader : MonoBehaviour
{
    public string characterconde;
    Abillity currentability;

    private void Awake()
    {
        currentability = new Abillity();
        currentability.hp = GetFullHP();
        currentability.ad = GetFullAD();
        currentability.dp = GetFullDP();
        currentability.spd = GetFullSPD();

    }

    public int GetFullHP()
    {
        return CSVReader.dataList[characterconde].hp;
    }

    public int GetFullAD()
    {
        return CSVReader.dataList[characterconde].ad;
    }

    public int GetFullDP()
    {
        return CSVReader.dataList[characterconde].dp;
    }

    public int GetFullSPD()
    {
        return CSVReader.dataList[characterconde].spd;
    }
}
