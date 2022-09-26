using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIModule : MonoBehaviour
{
    MainModule mainModule;

    public float patrolTime = 10;
    public float aggroRange = 10;

    public float attackRange = 2f;
    public float safeAttackRange = 1f;

    public bool inAttackRange = false;

    Vector3[] waypoint = new Vector3[4];


    int index = 0;
    private void Start()
    {
        mainModule = GetComponent<MainModule>();
        mainModule.currentUpdate = mainModule.StartCoroutine(Tick());

        InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);

        for(int i = 0; i<4; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            NavMeshHit hit = new NavMeshHit();
            NavMesh.SamplePosition(randomPosition, out hit, 10, NavMesh.AllAreas);
            waypoint[i] = hit.position;
        }
       

    }

    void Patrol()
    {
        index = (index+1)%4;
    }

    public IEnumerator Tick()
    {
        while (true)
        {
            if (!gameObject.activeSelf)
            {
                yield break;
            }

            mainModule.agent.isStopped = false;
            mainModule.agent.destination = waypoint[index];
            mainModule.agent.speed = mainModule.speed;

            if (MainModule.player != null && Vector3.Distance(transform.position, MainModule.player.transform.position) < aggroRange)
            {
                mainModule.agent.speed = mainModule.speed;
                mainModule.agent.destination = MainModule.player.transform.position;
            }
            if (MainModule.player != null & Vector3.Distance(transform.position, MainModule.player.transform.position) < safeAttackRange)
            {
                mainModule.agent.speed = 0;
                mainModule.agent.destination = transform.position;
                mainModule.agent.isStopped = true;
                inAttackRange = true;
            }

            if (MainModule.player != null && Vector3.Distance(transform.position, MainModule.player.transform.position) < attackRange && inAttackRange)
            {
                mainModule.agent.speed = 0;
                mainModule.agent.destination = transform.position;
                mainModule.agent.isStopped = true;

                yield return new WaitUntil(() => mainModule.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            }
            else
            {
                inAttackRange = false;
            }
        }

    }
}
