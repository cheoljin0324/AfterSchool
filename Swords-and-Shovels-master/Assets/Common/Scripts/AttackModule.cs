using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModule : MonoBehaviour
{
    public Collider attackCollider;
    public float range;
    MainModule mainModule;
    public void Start()
    {
        mainModule = GetComponent<MainModule>();
    }


    public void Attack()
    {
        if (!mainModule.enabled)
            return;

        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag!=gameObject.tag)
            {
                float distance;
                Vector3 direction;
                if (Physics.ComputePenetration(attackCollider,
                    transform.position,
                    transform.rotation,
                    cols[i],
                    cols[i].transform.position,
                    cols[i].transform.rotation,
                    out direction,
                    out distance
                    ))
                {
                    cols[i].GetComponent<HPModule>()?.Damage(mainModule.ad);
                }
            }
        }
    }
}
