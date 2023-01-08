using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField] private GameObject parentOfAnimation;
    [SerializeField] private GameObject current;
    [SerializeField] private GameObject finale;
    [SerializeField] private GameObject creditsTime;
    private int currentIndex = 1;

    private void Start()
    {
        StartCoroutine(FadeOutOrIn());
    }

    IEnumerator FadeOutOrIn(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Color objectColor = current.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (current.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                current.GetComponent<Image>().color = objectColor;
                yield return null;
            }

            
            
            if (currentIndex == 16)
            {
                StartCoroutine(WaitForCredits());
            }
            else
            {
                current = parentOfAnimation.transform.GetChild(currentIndex).gameObject;
                currentIndex++;
                StartCoroutine(FadeOutOrIn());
            }
                
        }
    }

    IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(2.0f);
        finale.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        creditsTime.SetActive(true);
    }
    
}
