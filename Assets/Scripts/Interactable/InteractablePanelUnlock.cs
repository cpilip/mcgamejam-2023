using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePanelUnlock : Interactable
{
    [SerializeField] 
    private GameObject wiresGameObject;

    public void Start()
    {
        wiresGameObject.GetComponent<Collider2D>().enabled = false;
    }

    public override void InteractWith()
    {

        // TODO: check if key has been enabled in UI
            Debug.Log("Unlocked Panel");
            // change sprite to open

            wiresGameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;

    }

}
