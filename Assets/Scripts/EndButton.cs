using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private GameObject blackOut;

    // Not active
    void onStart(){
        isActive = false;
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Game Over!");

        if (otherObj.gameObject.tag == "Player" && this.isActive)
        {
            RatAnimator.Instance.ResetInitialized();

            StartCoroutine(FadeOutBlackSquare());

            
        }

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
            SceneManager.LoadScene("EndSceneCredits");
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
