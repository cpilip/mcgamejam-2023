using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableWall : Dashable
{
    public override void DashThrough()
    {
        Debug.Log("Destructing this wall " + this.gameObject.name);
        this.transform.parent.gameObject.SetActive(false);
    }
}
