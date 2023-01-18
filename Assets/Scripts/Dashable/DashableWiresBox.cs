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
            this.transform.parent.GetComponent<Collider2D>().enabled = false;
           

            GameObject wiresBox = gameObject.transform.parent.gameObject;
            wiresBox.GetComponent<WiresBoxVisuals>().nextSprite();

            //  this.transform.parent.GetComponent<Collider2D>().enabled = true;

            // GameObject coat = gameObject.transform.parent.gameObject;
            // coat.GetComponent<SpriteRenderer>().sprite = otherSprite;
    	}
        // TODO swap sprite
    }
}
