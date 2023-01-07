using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum InteractableLoreType {
    Note,
    Audio
}

public class InteractableLore : Interactable
{
    [SerializeField] private InteractableLoreType loreType;

    public override void InteractWith()
    {
        if (loreType == InteractableLoreType.Note)
        {
            Debug.Log("We read the note!");
        }
        else if (loreType == InteractableLoreType.Audio)
        {
            Debug.Log("We listen to the audio!");
        }
    }

}
