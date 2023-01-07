using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    bool poweredOn = true;

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "Player") {
            Debug.Log("ded");
            // dies
        }
    }

    void TurnOffLaser()
    {
        this.poweredOn = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
