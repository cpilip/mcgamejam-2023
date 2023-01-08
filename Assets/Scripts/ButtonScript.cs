using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (otherObj.gameObject.tag == "Player" && !this.isActive)
        {
            Debug.Log("Is On!");

            this.isActive = true;

            //Room check here
            //GameObject shadow = otherObj.gameObject.transform.get;
            //objectToChange = shadow;
            if (SceneManager.GetActiveScene().name == "Room4")
            {
                otherObj.transform.GetChild(0).gameObject.SendMessage("applyButtonPower");;
            }
            
            if (objectToChange) 
            {
                objectToChange.SendMessage("applyButtonPower");
            }
        }

    }

    public bool isPressed()
    {
        return isActive;
    }

    public void setPressed(bool active)
    {
        this.isActive = active;
    }

}
