using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemModule : MonoBehaviour
{


    private static ItemModule selectedItem;
    public static ItemModule SelectedItem
    {

        get 
        {
            return selectedItem;
        }

        set 
        {
            if(selectedItem!=null)
            {
                value.text.color = Color.black;
                value.textBg.color = Color.white;
            }
            if (value != null)
            {
                value.text.color = Color.white;
                value.textBg.color = Color.black;

            }
            selectedItem = value;
        }


    }


    public Text text;
    public Image textBg;

    //getting Item
    public string data;
    public int num;

    public void SetItem()
    {
        //아이템 데이타
    }

    public void GetItem()
    {

        if (ItemSpawner.instance.itemInventory.ContainsKey(data))
            ItemSpawner.instance.itemInventory[data] += num;
        else
            ItemSpawner.instance.itemInventory.Add(data, num);


        gameObject.SetActive(false);
        selectedItem = null;
    }




}
