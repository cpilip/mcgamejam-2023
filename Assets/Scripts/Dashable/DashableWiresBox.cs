using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableWiresBox : Dashable
{
    bool canDash = false;

    public void SetCanDash(bool value)
    {
        canDash = value;
    }

    public override void DashThrough()
    {
        if (canDash)
        {
            canDash = false;
            Debug.Log("Open box and display chewable wires" + this.gameObject.name);
            this.transform.gameObject.tag = "Interactable";
    	}
        // TODO swap sprite
    }
}
