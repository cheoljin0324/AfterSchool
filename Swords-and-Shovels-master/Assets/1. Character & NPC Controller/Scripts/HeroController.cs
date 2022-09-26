using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{

    public const string idleAndRun = "IdleRun";
    public const string attack1 = "Attack1";
    public const string attack2 = "Attack2";

    Animator animator;
    NavMeshAgent agent;
    AttackModul attackmodul;

    float angularSpeed;
    float speed;

    void Awake()
    {
        attackmodul = GetComponent<AttackModul>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        angularSpeed = agent.angularSpeed;
        speed = agent.speed;
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);
        for(int i = 0; i<cols.Length; i++)
        {
            cols[i].GetComponent<Trigger>()?.EnterTrigger();
        }
    }

    public void RightClick(Vector3 destination)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleAndRun))
        {
            agent.isStopped = false;
            agent.angularSpeed = angularSpeed;
            agent.speed = speed;
            agent.destination = destination;
        }
    }

    public void LeftClick(Vector3 target)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleAndRun))
        {
            agent.isStopped = true;
            agent.angularSpeed = 0;
            agent.speed = 0;
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((target - transform.position).z, (target - transform.position).x ) * Mathf.Rad2Deg, 0);
            animator.Play("Attack1");
            animator.Update(0);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(attack1) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((target - transform.position).z, (target - transform.position).x )* Mathf.Rad2Deg, 0);
            animator.Play("Attack2");
            animator.Update(0);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(attack2) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((target - transform.position).z, (target - transform.position).x )* Mathf.Rad2Deg, 0);
            animator.Play("Attack3");
            animator.Update(0);
        }
    }
}
