using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public virtual void EnterTrigger()
    {
        gameObject.SetActive(false);
    }
}
