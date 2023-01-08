using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoor : MonoBehaviour
{
    [SerializeField]
    Sprite laserDoorOffSprite;
    // This is called when the wires are chewed through in the interactable panel in room 2
    public void ApplyWireEffect() 
    {
        Debug.Log("Door should be gone");
        this.GetComponent<SpriteRenderer>().sprite = laserDoorOffSprite;

        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            //Debug.Log("maze: player dead");
            MazeCreator.Instance.ResetRat();
            // dies
        }
    }
}
