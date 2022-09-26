using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModul : MonoBehaviour
{
    public Collider attackCollider;
    public float range;

    //gameobject?(���� null�̸� ������ ���°� �ƴ϶� null�� ��ȯ�Ѵ�.)
    public void Attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        for(int i = 0; i<cols.Length; i++)
        {
            if (cols[i].gameObject.CompareTag("Monster")) 
            {
                float distance;
                Vector3 direction;
                if (Physics.ComputePenetration(attackCollider, transform.position, transform.rotation, cols[i], cols[i].transform.position, cols[i].transform.rotation, out direction, out distance))
                {
                    cols[i].GetComponent<NPCController>().Hit();
                }
                }
        }
    }


}
