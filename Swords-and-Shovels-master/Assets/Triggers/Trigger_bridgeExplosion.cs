using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trigger_bridgeExplosion : Trigger
{
    public NavMeshObstacle obstacle;

    public Rigidbody[] trees;

    public void Start()
    {
        for (int i = 0; i < trees.Length; i++)
            trees[i].constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void EnterTrigger()
    {
        obstacle.enabled = true;
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].constraints = RigidbodyConstraints.None;
            trees[i].AddExplosionForce(300f, obstacle.transform.position, 10f);
        }
            
    }
}
