using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_opendoor : Trigger
{

    public Animator door;

    public override void EnterTrigger()
    {
        door.Play("Open");
        base.EnterTrigger();
    }

}
