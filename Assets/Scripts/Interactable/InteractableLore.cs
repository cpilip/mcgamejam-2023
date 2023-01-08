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
            
            // TEST CODE
            soundName = roomName;

            // FINAL, WITH "REWIND" TRACKS
            // if(hasBeenPlayed) 
            // {
            //     soundName = string.Concat(roomName, " rewind");
            // }
            // else
            // {
            //     soundName = roomName;
            //     hasBeenPlayed = true;
            // }

            CurrentSceneManager.Instance.SwapToMaze();

            FindObjectOfType<AudioManagerScript>().Play(soundName);

            // TEST CODE

    }

}
