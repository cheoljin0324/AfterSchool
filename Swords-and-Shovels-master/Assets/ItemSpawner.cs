using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{

    public static ItemSpawner instance;


    // Start is called before the first frame update
    public float range = 3f;
    public GameObject item;


    public Dictionary<string,int> itemInventory;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);

    }

    public void Spawn(string _name, Vector3 _vec)
    {

        Vector3 finalPos = Vector3.zero;
        bool isin = false;
        int count = 0;
        do
        {
            count++;
            isin = false;
            float randomAngle = Random.Range(0, 2 * Mathf.PI);
            Vector3 summonPos = transform.position + Random.Range(0, range) * new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(summonPos, out hit, 0.5f, NavMesh.AllAreas))
            {
                Collider[] cols = Physics.OverlapCapsule(hit.position, hit.position + 3 * Vector3.up, item.GetComponent<SphereCollider>().radius);
                for (int j = 0; j < cols.Length; j++)
                {
                    if (cols[j].GetComponent<ItemModule>() == null)
                        continue;
                    else
                    {
                        isin = true;
                        break;
                    }
                }
                finalPos = hit.position;
            }
            else
                isin = true;
        } while (isin && count < 20);

        item.transform.position = finalPos;
        item.SetActive(true);
    }


}
