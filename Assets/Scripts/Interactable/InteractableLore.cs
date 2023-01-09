using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableLore : Interactable
{
    //private bool hasBeenPlayed;
    [SerializeField] public string roomName;

    void Start()
    {
        //hasBeenPlayed = false;
    }

    public override void InteractWith()
    {
        
            Debug.Log("We listen to the audio!");

            // == NEED TO WORK OUT HOW TO AVOID ROOM1 AND ROOM1 REWIND OVERLAP ==
            // if(soundName = roomName && FindObjectOfType<AudioManagerScript>().isPlaying)
            // {
            //     hasBeenPlayed = false;
            // }
            
            // if(hasBeenPlayed) 
            // {
            //     soundName = string.Concat(roomName, " rewind");
            // }
            // else
            // {
            //     soundName = roomName;
            //     hasBeenPlayed = true;
            // }
        
            FindObjectOfType<AudioManagerScript>().Play(roomName);

            if(roomName=="Room4")
            {
               FindObjectOfType<AudioManagerScript>().Stop("BGM"); 
            }

            FindObjectOfType<SubtitleScript>().displaySubtitles();
            
    }

}
