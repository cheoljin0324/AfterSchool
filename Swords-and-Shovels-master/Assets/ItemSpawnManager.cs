using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    static ItemSpawnManager instance;

    public PullManager pullManager=null;

    private void Awake()
    {
        instance = this;
    }

    public void ItemSpawn(string itemType, Transform itemPos)
    {
        GameObject currentItem = pullManager.RecultPull(itemPos.position);
        currentItem.GetComponent<Item>().type = itemType;
    }



    //싱글톤
    public static ItemSpawnManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new ItemSpawnManager();
            }
            return instance;
        }
    }
}


