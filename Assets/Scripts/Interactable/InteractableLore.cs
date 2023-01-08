using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLore : Interactable
{
    private bool hasBeenPlayed;
    [SerializeField] private string roomName;
    private string soundName;

    void Start()
    {
        hasBeenPlayed = false;
    }

// TEST CODE
    // void Update()
    // {
    //     if (Input.GetKeyDown("space"))
    //     {
    //         InteractWith();
    //     }
    // }

    public override void InteractWith()
    {
        
            Debug.Log("We listen to the audio!");

            // WILL ADD "REWIND" TRACKS TO PLAY BACK ONLY THE HINT
            if(hasBeenPlayed) 
            {
                //soundName = string.Concat(roomName, " rewind");
                soundName = roomName;
            }
            else
            {
                soundName = roomName;
                hasBeenPlayed = true;
            }

            FindObjectOfType<AudioManagerScript>().Play(soundName);

    }

}
