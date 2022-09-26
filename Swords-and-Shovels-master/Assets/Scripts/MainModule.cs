using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainModule : MonoBehaviour
{
    public static MainModule player;

    public bool isPlayer;

    public Animator animator;
    public Collider collider;
    public NavMeshAgent agent;

    public Coroutine currentUpdate;

    public AIModule aiModule;
    public InputModule inputModule;
    public HPModule hpmodule;

    public float speed;
    public float angularSpeed;

    private void Awake()
    {
        if (isPlayer)
            player = this;

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        aiModule = GetComponent<AIModule>();
        inputModule = GetComponent<InputModule>();
        hpmodule = GetComponent<HPModule>();
        angularSpeed = agent.angularSpeed;
        speed = agent.speed;
    }

    private void Update()
    {
        if(aiModule == null)
        {
          
        }
        if(inputModule == null)
        {

        }
    }


}
