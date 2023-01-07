using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWires : Interactable
{
    [SerializeField]
    GameObject objectToChange;

    public override void InteractWith()
    {
        Debug.Log("Wires are chewed.");

        if (objectToChange) 
        {
            objectToChange.SendMessage("ApplyWireEffect");
        }
    }

    public void SetObjectToChange(GameObject obj)
    {
        if (obj)
        {
            this.objectToChange = obj;
        }
    }

}
