using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLabCoat : Interactable
{
    [SerializeField]
    GameObject popUpKeyCollected;

    public bool keyCollected = false;

    [SerializeField]
    Sprite otherSprite;

    public override void InteractWith()
    {
        Debug.Log("key collected!");
        popUpKeyCollected.GetComponent<FadingEffect>().canFade = true;
        keyCollected = true;

        GameObject coat = gameObject.transform.parent.gameObject;
        coat.GetComponent<SpriteRenderer>().sprite = otherSprite;

    }

    public void SetObjectToChange(GameObject obj)
    {
        if (obj)
        {
            this.popUpKeyCollected = obj;
        }
    }
}
