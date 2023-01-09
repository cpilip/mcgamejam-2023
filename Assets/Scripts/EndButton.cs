using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private GameObject blackOut;
    [SerializeField] private GameObject static1;
    [SerializeField] private GameObject static2;

[SerializeField] GameObject LoreObj;

    // Not active
    void onStart(){
        isActive = false;
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Game Over!");

        if (otherObj.gameObject.tag == "Player" && this.isActive)
        {

            RatAnimator.Instance.ResetInitialized();

            StartCoroutine(FadeInStatic());

            

            // just interact with 
            LoreObj.SendMessage("InteractWith");
            SceneManager.LoadScene("EndSceneCredits");
        }

    }

    IEnumerator FadeInStatic(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        blackOut.SetActive(true);
        yield return new WaitForSeconds(12f);
        blackOut.SetActive(false);
        yield return new WaitForSeconds(8f);

        Color objectColor = static1.GetComponent<Image>().color;
        float fadeAmount;

        while (static1.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            static1.GetComponent<Image>().color = objectColor;
            static2.GetComponent<Image>().color = objectColor;

            if (static1.gameObject.activeSelf)
            {
                static1.gameObject.SetActive(false);
                static2.gameObject.SetActive(true);
            } else if (static2.gameObject.activeSelf)
            {
                static2.gameObject.SetActive(false);
                static1.gameObject.SetActive(true);
            }

            yield return null;
        }

        SceneManager.LoadScene("EndSceneCredits");
    }

    IEnumerator FadeOutBlackSquare(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackOut.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOut.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOut.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            
        }
        else
        {
            while (blackOut.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOut.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }

    public void ActivateEndButton()
    {
        isActive = true; 
    }
}
