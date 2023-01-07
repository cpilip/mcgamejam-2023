using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lasers : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            // Reloads scene 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
