using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggerActions
{
    string openState = "DoorOpen";
    string closeState = "DoorClose";

    public override void TargetEntered(Collider other)
    {
        GetComponent<Animator>().Play(openState, 0);
    }

    public override void TargetLost()
    {
        GetComponent<Animator>().Play(closeState, 0);
    }
}
