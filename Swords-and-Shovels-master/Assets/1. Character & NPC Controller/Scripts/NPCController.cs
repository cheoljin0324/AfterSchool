using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10;
    public float aggroRange = 10;
    public float attackRange = 2.0f;
    public float safeAttackRange = 2.0f;
    public Transform[] waypoints;
    public Collider AttackCol;

    public LayerMask layer;

    bool inAttackRange = false;

    int index;
    float speed, agentSpeed;
    Transform player;

    Animator animator;
    NavMeshAgent agent;

    public Coroutine updateCoroutine;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        updateCoroutine = StartCoroutine(Tick());

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    void Update()
    {
        speed = Mathf.Lerp(speed, agent.velocity.magnitude, Time.deltaTime * 10);
        animator.SetFloat("Speed", speed);
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    public IEnumerator Tick()
    {
        while (true)
        {
            if (!gameObject.activeSelf)
            {
                yield break;
            }
            agent.destination = waypoints[index].position;
            agent.speed = agentSpeed / 2;

            if (player != null && Vector3.Distance(transform.position, player.transform.position) < aggroRange)
            {
                agent.speed = agentSpeed;
                agent.destination = player.position;
            }
            if (player != null & Vector3.Distance(transform.position, player.transform.position) < safeAttackRange)
            {
                agent.speed = 0;
                agent.destination = transform.position;
                agent.isStopped = true;
                inAttackRange = true;
            }

            if (player != null && Vector3.Distance(transform.position, player.transform.position) < attackRange && inAttackRange)
            {
                agent.speed = 0;
                agent.destination = transform.position;
                agent.isStopped = true;

                yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            }
            else
            {
                inAttackRange = false;
            }
        }
      
    }

    void Attack()
    {
        Collider[] col;
        col = Physics.OverlapSphere(transform.position, attackRange, layer);
        for(int i = 0; i<col.Length; i++)
        {
            if (col[i].CompareTag("Player"))
            {
                float distance;
                Vector3 direction;
                if(Physics.ComputePenetration(AttackCol, transform.position, transform.rotation, col[i], col[i].transform.position, col[i].transform.rotation, out direction, out distance))
                {
                    col[i].GetComponent<HeroController>().hp -= 1;
                    Debug.Log("Damage");
                }
            }
        }
    }

    public void Hit()
    {
        gameObject.SetActive(false);
    }
}
