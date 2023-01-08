using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLabCoat : Interactable
{
    [SerializeField]
    GameObject popUpKeyCollected;

    public bool keyCollected = false;

    public override void InteractWith()
    {
        Debug.Log("key collected!");
        popUpKeyCollected.GetComponent<FadingEffect>().canFade = true;
        keyCollected = true;

    }

    public void SetObjectToChange(GameObject obj)
    {
        if (obj)
        {
            this.popUpKeyCollected = obj;
        }
    }
}
