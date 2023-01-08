using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoor : MonoBehaviour
{
    private bool powered = true;

    // This is called when the wires are chewed through in the interactable panel in room 2
    public void ApplyWireEffect() 
    {
        Debug.Log("Door should be gone");
        this.powered = false;
        // Change sprite to non-powered/delete it??, also remove collider

        this.GetComponent<BoxCollider2D>().enabled = false;

    }
}
