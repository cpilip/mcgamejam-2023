using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // For the code combo: list of booleans (0,0,0,0) -> (0,1,0,0)
// to check if output corresponds to what we are getting in the right order


    private bool isActive;

    [SerializeField]
    GameObject gameObjectToPower;

    // Not active
    void onStart(){
        isActive = false;

        Debug.Log("Is Off!");
    }

    void OnTriggerEnter2D(Collider2D otherObj) {

        if (otherObj.gameObject.tag == "Player")
        {

            // this.gameObjectToPower.isActive = true;

            this.isActive = true;

            Debug.Log("Is On!");
        }

    }

}
