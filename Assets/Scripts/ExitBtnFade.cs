using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ExitBtnFade : MonoBehaviour {
 
    void Awake()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    void Start()
    {
        StartCoroutine(ExitFade());
    }
    
    IEnumerator ExitFade()
    {
        yield return new WaitForSeconds(85.5f);
        this.gameObject.GetComponent<Animator>().enabled = true;
        this.gameObject.GetComponent<Animator>().Play("exitFadeIn");
    }
 
}