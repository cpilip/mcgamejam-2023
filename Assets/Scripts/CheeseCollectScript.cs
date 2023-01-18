using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCollectScript : MonoBehaviour
{
    private GameObject player;
    void OnTriggerEnter2D(Collider2D otherObj) {

        if (otherObj.gameObject.tag == "Player")
        {
            // player will have some kind of add cheese function to add stamina/increment counter/etc
            //collision.gameObject.playercontroller.addCheese();
            player = otherObj.gameObject;
            // Destroy the cheese
            RatAnimator.Instance.TryNibble();
            StartCoroutine(LetAnimationPlay());
        }

    }

    IEnumerator LetAnimationPlay()
    {
        player.GetComponent<RatMovement>().isLocked = true;
        yield return new WaitForSeconds(.75f);
        FindObjectOfType<AudioManagerScript>().Play("Nibble");
        yield return new WaitForSeconds(.75f);
        player.GetComponent<RatMovement>().isLocked = false;
        Destroy(this.gameObject);
    }

}
