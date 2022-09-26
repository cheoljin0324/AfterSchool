using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public void Awake()
    {
        ReadText();
    }

    public static Dictionary<string, Abillity> dataList;

    public static void ReadText()
    {
        dataList = new Dictionary<string, Abillity>();
        string text = Resources.Load<TextAsset>("ability").text;
        string[] lines = text.Split('\n');
        for(int i = 1; i<lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i].Trim()))
                continue;

            string[] cols = lines[i].Split('\t');
            Abillity newAbility;
            newAbility.hp = int.Parse(cols[1].Trim());
            newAbility.ad = int.Parse(cols[2].Trim());
            newAbility.dp = int.Parse(cols[3].Trim());
            newAbility.spd = int.Parse(cols[4].Trim());

            dataList.Add(cols[0], newAbility);

        }
    }
}

public struct Abillity { public int hp; public int ad; public int dp; public int spd; }

