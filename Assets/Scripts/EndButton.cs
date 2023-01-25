using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    private bool isActive = false;
    private bool isActive2 = false;
    [SerializeField] private GameObject blackOut;
    [SerializeField] private GameObject static1;
    [SerializeField] private GameObject static2;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject LoreObj;


    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Game Over!");

        if (otherObj.gameObject.tag == "Player" && this.isActive2)
        {
            otherObj.gameObject.GetComponent<RatMovement>().isLocked = true;
            
            FindObjectOfType<AudioManagerScript>().Play("Alarm");
            CameraChanger.Instance.SwapToCutsceneCamera();

            StartCoroutine(FadeInStatic());

        }

    }

    IEnumerator FadeInStatic(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        RatAnimator.Instance.TryLookAround();
        
        yield return new WaitForSeconds(6f);

        anim.enabled = true;
        yield return new WaitForSeconds(4f);

        //FindObjectOfType<AudioManagerScript>().Stop("Alarm");

        //yield return new WaitForSeconds(2.5f);
        // just interact with 
        LoreObj.SendMessage("InteractWith");
        SceneManager.LoadScene("EndSceneCredits");
    }

    public void ActivateEndButton()
    {
        isActive = true; 
    }

    void Update()
    {
        if (isActive)
        {
            isActive = false;
            isActive2 = true;
            StartCoroutine(WaitASec());
        }
    }

    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
