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



    //�̱���
    public static ItemSpawnManager Instance
    {
        get
        {
            if (null == instance)
            {
                //���� �ν��Ͻ��� ���ٸ� �ϳ� �����ؼ� �־��ش�.
                instance = new ItemSpawnManager();
            }
            return instance;
        }
    }
}


