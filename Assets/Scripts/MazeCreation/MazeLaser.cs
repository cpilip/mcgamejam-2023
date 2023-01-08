using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLaser : MonoBehaviour
{
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
