using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIModule : MonoBehaviour
{
    MainModule mainmodule;
    public float patrolTime = 10;
    public float aggroRange = 10;

    public float attackRange = 2f;
    public float safeAttackRange = 1f;

    public bool inAttackRange = false;
    Vector3[] waypoint = new Vector3[4];
    // Start is called before the first frame update

    int index = 0;
    void Awake()
    {
        mainmodule = GetComponent<MainModule>();

        StartCoroutine(Patrol());

        mainmodule.currentUpdate = mainmodule.StartCoroutine(Tick());

        InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);

        for (int i=0;i<4;i++)
        { 
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            NavMeshHit hit = new NavMeshHit();
            NavMesh.SamplePosition(randomPosition, out hit, 10, NavMesh.AllAreas);
            waypoint[i] = hit.position;
        }

    }
    IEnumerator Patrol()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            index = (index + 1) % 3;
        }
        index = (index+1)%4;
    }

    public IEnumerator Tick()
    {


        while (true)
        {
            if (!gameObject.activeSelf)
                yield break;


            mainmodule.SetDestination(waypoint[index], mainmodule.speed / 2);

            if (MainModule.player != null && Vector3.Distance(transform.position, MainModule.player.transform.position) < aggroRange)
            {
                mainmodule.SetDestination(MainModule.player.transform.position, mainmodule.speed);
            }

            if (MainModule.player != null && Vector3.Distance(transform.position, MainModule.player.transform.position) < safeAttackRange)
            {
                mainmodule.Stop();
                inAttackRange = true;

            }

            if (MainModule.player != null && Vector3.Distance(transform.position, MainModule.player.transform.position) < attackRange && inAttackRange)
            {

                mainmodule.Stop();

                mainmodule.TryAttack(Vector3.zero);
                yield return mainmodule.StartCoroutine(mainmodule.WaitUntilAttackEnd());


            }
            else
            {
                inAttackRange = false;
            }
            yield return null;
        }


    }
}
