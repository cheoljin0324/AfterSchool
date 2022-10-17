using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainModule : MonoBehaviour
{
    public static MainModule player;

    public bool isPlayer=false;

    public Animator animator;
    public Collider collider;
    public NavMeshAgent agent;

    public Coroutine currentUpdate;

    public float angularSpeed;
    public float speed;

    public AIModule aimodule;
    public InputModule inputmodule;
    public HPModule hpmodule;
    public AttackModule attackModule;
    public ItemSpawnManager itemSpawnManager;

    public bool isItem = true;
    public int ad = 5;

    void Awake()
    {
        if (isPlayer)
            player = this;


        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        aimodule = GetComponent<AIModule>();
        inputmodule = GetComponent<InputModule>();
        hpmodule = GetComponent<HPModule>();
        attackModule = GetComponent<AttackModule>();
        angularSpeed = agent.angularSpeed;
        speed = agent.speed;
    }



    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<Trigger>()?.EnterTrigger();

        }

    }



    public void Stop()
    {
        agent.isStopped = true;
        agent.angularSpeed = 0;
        agent.speed = 0;
        agent.destination = transform.position;
    }

    public void SetDestination(Vector3 _dest, float? _speed=null)
    {
        agent.isStopped = false;
        agent.angularSpeed = angularSpeed;
        if (_speed == null)
            agent.speed = speed;
        else
            agent.speed = _speed.Value;
       agent.destination = _dest;
    }

    public void MoveTo(Vector3 _dest,float? speed=null)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAndRun"))
        {

            SetDestination(_dest,speed);
        }

    }



    

    public void TryAttack(Vector3 _clickingPos)
    {
        if (gameObject.CompareTag("Monster"))
        {
            Stop();
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((player.transform.position - transform.position).z, (player.transform.position - transform.position).x) * Mathf.Rad2Deg, 0);
            animator.Play("Attack");
            animator.Update(0);

            return;
        }




        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAndRun"))
        {

            Stop();
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((_clickingPos - transform.position).z, (_clickingPos - transform.position).x) * Mathf.Rad2Deg, 0);
            animator.Play("Attack1");
            animator.Update(0);


        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {

            Stop();
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((_clickingPos - transform.position).z, (_clickingPos - transform.position).x) * Mathf.Rad2Deg, 0);
            animator.Play("Attack2");
            animator.Update(0);


        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {

            Stop();
            transform.rotation = Quaternion.Euler(0, 110 - Mathf.Atan2((_clickingPos - transform.position).z, (_clickingPos - transform.position).x) * Mathf.Rad2Deg, 0);
            animator.Play("Attack3");
            animator.Update(0);


        }





    }


    public IEnumerator WaitUntilAttackEnd()
    {
        yield return new WaitUntil(() =>
                    !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"));
    }




    public void StartLogicAgain()
    {
        if (currentUpdate != null)
            StopCoroutine(currentUpdate);

        if (aimodule != null)
            currentUpdate = StartCoroutine(aimodule.Tick());
    
    
    
    }
    public void Damage()
    {

        if (gameObject.CompareTag("Player"))
            return;

        if (currentUpdate != null)
            StopCoroutine(currentUpdate);
        currentUpdate = StartCoroutine(IEDamage());

    
    
    }

    //경직
    IEnumerator IEDamage()
    {
        //NpcCotroller의 코루틴을 멈춤.

        Stop();

        animator.Play("Hit",0,0);
        animator.Update(0);
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"));
        //NpcCotroller의 코루틴을 다시시작
        if(aimodule)
        currentUpdate =
           StartCoroutine(aimodule.Tick());

    }

    public void OnDisable()
    {
        if(agent.isActiveAndEnabled)
        Stop();
    }

    
}
