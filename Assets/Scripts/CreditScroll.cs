using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CreditScroll : MonoBehaviour {
 
    void Awake()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    void Start()
    {
        StartCoroutine(RollCredits());
    }
    
    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(62.5f);
        this.gameObject.GetComponent<Animator>().enabled = true;
        this.gameObject.GetComponent<Animator>().Play("creditsScroll");
    }
 
}