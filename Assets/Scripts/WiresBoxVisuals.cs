using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresBoxVisuals : MonoBehaviour
{
    [SerializeField]
    Sprite openedSprite;

    [SerializeField]
    Sprite chewedSprite;

    private int index; 

    public void nextSprite() {
        if (index == 0) {
            this.GetComponent<SpriteRenderer>().sprite = openedSprite;
        } else if (index == 1) {
            this.GetComponent<SpriteRenderer>().sprite = chewedSprite;
        }

        index++;
    }  
}
