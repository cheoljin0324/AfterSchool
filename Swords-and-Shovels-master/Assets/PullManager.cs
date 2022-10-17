using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public int amount;
    public GameObject pullObj;
    Queue<GameObject> Q = new Queue<GameObject>();

    private void Awake()
    {
        SetPull();
    }

    public void SetPull()
    {
            for(int j = 0; j<amount; j++)
            {
                GameObject setPr = Instantiate(pullObj);
                Q.Enqueue(setPr);
                setPr.SetActive(false);
            }
    }

    public GameObject RecultPull()
    {
        if(Q.Count == 1)
        {
            AddPull();
        }
        GameObject nowObj = Q.Dequeue();
        nowObj.SetActive(true);

        return nowObj;
    }

    public GameObject RecultPull(Vector3 pos)
    {
        if (Q.Count == 1)
        {
            AddPull();
        }
        GameObject nowObj = Q.Dequeue();
        nowObj.transform.position = pos;
        nowObj.SetActive(true);

        return nowObj;
    }

    public void CullObject(GameObject setObject)
    {
        Q.Enqueue(setObject);
        setObject.SetActive(false);
    }

    public void AddPull()
    {
        GameObject pr = Instantiate(pullObj);
        pr.SetActive(false);
    }
}

