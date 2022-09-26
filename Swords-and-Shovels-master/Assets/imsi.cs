using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imsi : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i].AddExplosionForce(300f, transform.position, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
