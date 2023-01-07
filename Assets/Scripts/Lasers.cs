using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "Player") {
            # dies
        }
    }
}
