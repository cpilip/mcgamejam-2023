using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    Sprite laserOffSprite;

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "Player") {
            Debug.Log("ded");
            // dies
            MazeCreator.Instance.ResetRat();
        }
    }

    void TurnOffLaser()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = laserOffSprite;
    }

    public void ApplyWireEffect() 
    {
        this.GetComponent<SpriteRenderer>().sprite = laserOffSprite;

        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void applyButtonPower() {
        TurnOffLaser();
    }
}
