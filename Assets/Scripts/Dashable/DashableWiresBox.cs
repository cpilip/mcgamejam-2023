using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableWiresBox : Dashable
{

    public override void DashThrough()
    {
        Debug.Log("Open box and display chewable wires" + this.gameObject.name);
        this.transform.parent.gameObject.tag = "Interactable";

        // TODO swap sprite
    }
}
