using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ItemData
{
    [SerializeField]
    GameObject mainModule;
    PullManager pull;
    InventoryModule inventory;

    private void Start()
    {
        pull = FindObjectOfType<PullManager>();
        inventory = FindObjectOfType<InventoryModule>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")&& mainModule.GetComponent<MainModule>().isItem == true)
        {
            pull.CullObject(gameObject);
            inventory.GetItme(this);
        }
    }
}
