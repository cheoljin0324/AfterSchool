using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_closeDoor : Trigger
{
    public Animator door;
    public override void EnterTrigger()
    {
        door.Play("Close");
        base.EnterTrigger();
       
    }
}
