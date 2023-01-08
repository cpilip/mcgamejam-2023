using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCollectScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D otherObj) {

        if (otherObj.gameObject.tag == "Player")
        {
            // player will have some kind of add cheese function to add stamina/increment counter/etc
            //collision.gameObject.playercontroller.addCheese();

            // Destroy the cheese
            RatAnimator.Instance.TryNibble();
            Destroy(this.gameObject);
            FindObjectOfType<AudioManagerScript>().Play("Nibble");
        }

    }

}
