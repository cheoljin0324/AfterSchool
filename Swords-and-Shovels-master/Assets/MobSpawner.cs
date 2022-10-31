using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : Trigger
{
    public GameObject[] mosters;
    public GameObject[] summonsMonsters;

    public float range = 3f;

    public Trigger trigger;

    bool summoned = false;
    Collider col;

    public void Start()
    {
        summonsMonsters = new GameObject[mosters.Length];
        col = GetComponent<Collider>();
    }
    public override void EnterTrigger()
    {
        for (int i = 0; i < mosters.Length; i++)
            Spawn(i);
        summoned = true;
        col.enabled = false;
    }
    public void Spawn(int i)
    {
        summonsMonsters[i] = Instantiate(mosters[i]);
        summonsMonsters[i].SetActive(false);

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
                Collider[] cols = Physics.OverlapCapsule(hit.position, hit.position + 3 * Vector3.up, summonsMonsters[i].GetComponent<CapsuleCollider>().radius);
                for (int j = 0; j < cols.Length; j++)
                {
                    if (cols[j].GetComponent<MainModule>() == null)
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

        summonsMonsters[i].transform.position = finalPos;
        summonsMonsters[i].SetActive(true);

    }


    public void Update()
    {
        bool end = true;
        if (summoned)
        {
            for (int i = 0; i < summonsMonsters.Length; i++)
            {
                if (summonsMonsters[i].activeSelf)
                    end = false;
            }

        }
        else
        {
            end = false;

        }
        if (end)
        {
            trigger.EnterTrigger();
            gameObject.SetActive(false);
        }

    }




}