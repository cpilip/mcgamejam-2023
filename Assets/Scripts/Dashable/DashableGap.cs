using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableGap : Dashable
{
    public override void DashThrough()
    {
        Debug.Log("Dashing through gap " + this.gameObject.name);
        // TODO Kill player
    }
}
