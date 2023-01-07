using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // For the code combo: list of booleans (0,0,0,0) -> (0,1,0,0)
    // to check if output corresponds to what we are getting in the right order

    private bool isActive;

    [SerializeField]
    GameObject objectToChange;

    // Not active
    void onStart(){
        isActive = false;

        Debug.Log("Is Off!");
    }

    void OnTriggerEnter2D(Collider2D otherObj) {

        if (otherObj.gameObject.tag == "Player")
        {
            Debug.Log("Is On!");

            this.isActive = true;

            if (objectToChange) 
            {
                objectToChange.SendMessage("applyButtonPower");
            }
        }

    }

}
