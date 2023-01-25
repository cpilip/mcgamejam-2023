using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableGlass : Dashable
{
    [SerializeField]
    Sprite glassCrackedABit;
    [SerializeField]
    Sprite glassCrackedALot;
    [SerializeField]
    Sprite glassOpen;

    int counter = 0;
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
            Debug.Log("Dashing through glass" + this.gameObject.name);
            counter++;

            switch (counter)
            {
                case 1:
                    // Sprite a lil cracked
                    Debug.Log("cracked the glass a bit" + this.gameObject.name);
                    this.GetComponentInParent<SpriteRenderer>().sprite = glassCrackedABit;
                    break;
                case 2:
                    // Sprite cracked glass
                    Debug.Log("cracked the glass a lot" + this.gameObject.name);
                    this.GetComponentInParent<SpriteRenderer>().sprite = glassCrackedALot;
                    break;
                case 3:
                    this.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    this.GetComponentInParent<BoxCollider2D>().enabled = false;
                    this.GetComponentInParent<SpriteRenderer>().sprite = glassOpen;
                    this.gameObject.transform.GetChild(0).GetComponent<EndButton>().ActivateEndButton();
                    break;
            }
        }
    }
}
