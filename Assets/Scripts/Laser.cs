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
            CurrentSceneManager.Instance.ResetRat();
        }
    }

    void TurnOffLaser()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = laserOffSprite;
        this.transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<AudioManagerScript>().Play("ButtonPress");
    }

    public void ApplyWireEffect() 
    {
        this.GetComponent<SpriteRenderer>().sprite = laserOffSprite;

        this.GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<AudioManagerScript>().Play("ButtonPress");
    }

    public void applyButtonPower() {
        TurnOffLaser();
    }
}
