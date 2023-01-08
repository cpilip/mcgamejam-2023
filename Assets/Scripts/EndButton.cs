using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    private bool isActive;

[SerializeField] GameObject LoreObj;

    // Not active
    void onStart(){
        isActive = false;
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Game Over!");

        if (otherObj.gameObject.tag == "Player" && this.isActive)
        {
            // just interact with 
            LoreObj.SendMessage("InteractWith");
            SceneManager.LoadScene("EndSceneCredits");
        }

    }

    public void ActivateEndButton()
    {
        isActive = true; 
    }
}
