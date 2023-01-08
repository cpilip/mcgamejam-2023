using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadingEffect : MonoBehaviour
{
    public bool canFade = false;
    public float fadeInSpead;
    public float fadeOutSpead;
    public float counterDown;
    
    private bool fadeInCompleted;

    public Component[] objectRenderers;

    [SerializeField] TextMeshProUGUI text;

    public void Start()
    {
        canFade = false;
        fadeInSpead = 0.005f;
        fadeOutSpead = 0.1f;
        counterDown = 5f;
        fadeInCompleted = false;
    }
    public void Update()
    {
        if (canFade)
        {
            StartCoroutine(FadeIn());
        }

        if (fadeInCompleted)
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
        objectRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        
        foreach (SpriteRenderer render in objectRenderers) {
            Debug.Log(render.color.a);
            while (render.color.a < 1)
            {
                Color objectColor = render.color;
                float fadeAmount = objectColor.a + (fadeInSpead * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                render.color = objectColor;
                yield return null;
            }
        }
        while (text.color.a < 1)
        {
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
        SpriteRenderer object1 = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer object2 = this.transform.GetChild(1).GetComponent<SpriteRenderer>();
    
        while (text.color.a > 0)
        {
            object1.color = getNextColor(object1.color);
            object2.color = getNextColor(object1.color);
            text.color = getNextColor(text.color);
            yield return null;
        }
        
        canFade = false;
    }

    public Color getNextColor(Color color) {
        Color objectColor = color;
        float fadeAmount = objectColor.a - (fadeOutSpead * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        return objectColor;
    }
}
