using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField] private GameObject parentOfAnimation;
    [SerializeField] private GameObject current;
    [SerializeField] private GameObject finale;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject fin;
    [SerializeField] private Animator anim;
    private int currentIndex = 1;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
        //FindObjectOfType<AudioManagerScript>().Stop("Alarm");
        StartCoroutine(WaitABit());
    }
    IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(5.0f); 
        StartCoroutine(FadeAnEyeIn());
    }

    IEnumerator FadeAnEyeIn(bool fadeToBlack = true, float fadeSpeed = .75f)
    {
        //yield return new WaitForSeconds(3.0f);
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
                StartCoroutine(FadeAnEyeIn());
            }
                
        }
    }

    IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(4.0f);
        finale.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        credits.SetActive(true);
        anim.enabled = true;
        yield return new WaitForSeconds(40.0f);
        fin.SetActive(true);
        //Continue credits here
    }
    
    public void EndGame()
    {
        Application.Quit();
    }
}
