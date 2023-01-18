using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FadingEffect : MonoBehaviour
{
    public bool canFade = false;
    public float fadeInSpead;
    public float fadeOutSpead;
    public float counterDown;
    
    private bool fadeInCompleted;

    [SerializeField]
    TextMeshProUGUI text;

    
    [SerializeField]
    Image panel;

    [SerializeField]
    Image key;

    public void Start()
    {
        canFade = false;
        fadeInSpead = 0.01f;
        fadeOutSpead = 0.01f;
        counterDown = 4f;
        fadeInCompleted = false;

        Color objectColor = text.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        text.color = objectColor;
        panel.GetComponent<CanvasRenderer>().SetAlpha(0f);
        key.GetComponent<CanvasRenderer>().SetAlpha(0f);
        
    }
    public void Update()
    {
        if (canFade)
        {
            StartCoroutine(FadeIn());
        }

        else if (fadeInCompleted)
        {
            counterDown -= Time.deltaTime;

            if (counterDown < 0)
            {
                StartCoroutine(FadeOut());
            }
        }
    }

    public IEnumerator FadeIn()
    {
        panel.CrossFadeAlpha(1f,0.2f,false);

        while (text.color.a < 1)
        {   
            if (key.GetComponent<CanvasRenderer>().GetAlpha() != 1f)
            {
                key.CrossFadeAlpha(1f,1f,false);
            }
            Color objectColor = text.color;
            float fadeAmount = objectColor.a + (fadeInSpead * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            text.color = objectColor;

            yield return null;
        }
        
        canFade = false;
        fadeInCompleted = true;
    }
    
    public IEnumerator FadeOut()
    {
        key.CrossFadeAlpha(0f,0.8f,false);
        while (text.color.a > 0)
        {
            Color objectColor = text.color;
            float fadeAmount = objectColor.a - (fadeOutSpead * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            text.color = objectColor;

            yield return null;
        }

        panel.CrossFadeAlpha(0f,1f,false);

        canFade = false;
        fadeInCompleted = false;
    }
}
