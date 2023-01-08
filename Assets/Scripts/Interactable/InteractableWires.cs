using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWires : Interactable
{
    [SerializeField]
    GameObject objectToChange;

    [SerializeField]
    Sprite chewedSprite;

    public override void InteractWith()
    {
        Debug.Log("Wires are chewed.");

        if (objectToChange) 
        {
            objectToChange.SendMessage("ApplyWireEffect");
        }

        GameObject wiresBox = gameObject.transform.parent.gameObject;
        wiresBox.GetComponent<SpriteRenderer>().sprite = chewedSprite;
    }

    public void SetObjectToChange(GameObject obj)
    {
        if (obj)
        {
            this.objectToChange = obj;
        }
    }

}
