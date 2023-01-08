using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePanelUnlock : Interactable
{
    [SerializeField] 
    private GameObject wiresGameObject;
    [SerializeField]
    GameObject labCoat;

    [SerializeField]
    Sprite openedSprite;

    public void Start()
    {
        wiresGameObject.GetComponent<Collider2D>().enabled = false;
    }

    public override void InteractWith()
    {
        // check if key has been enabled in UI
        if (labCoat.GetComponent<InteractableLabCoat>().keyCollected == true)
        {
            Debug.Log("Unlocked Panel");
            // change sprite to open

             this.GetComponent<SpriteRenderer>().sprite = openedSprite;

            wiresGameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
