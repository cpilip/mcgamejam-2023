using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SubtitleScript : MonoBehaviour
{
    public TextMeshProUGUI subtitles;
    [SerializeField] private Image subtitleBackground;
    private string fileName;
    private bool running;

    void Start()
    {
        subtitles.enabled = false;
        subtitleBackground.enabled = false;
        running = false;
    }

    public void displaySubtitles()
    {
        if(running)
        {
            StopCoroutine("Subtitles");
        }
        StartCoroutine("Subtitles");
    }

    // reads subtitles from .txt files in Resources folder
    IEnumerator Subtitles()
    {
        subtitles.enabled = true;
        subtitleBackground.enabled = true;
        running = true;

        string currentRoom = GameObject.Find("Lore Note").GetComponent<InteractableLore>().roomName;
        
        fileName = currentRoom+"Subtitles";
        
        string[] linesArr = Resources.Load<TextAsset>(fileName).text.Split("\n"[0]); // this line taken directly from the code of Christina's previous teammate
        

        foreach(string line in linesArr)
        {
            subtitles.text = line.Split('@')[0];
            float subtitleDelay = float.Parse(line.Split('@')[1]);
            yield return new WaitForSeconds(subtitleDelay);
        }

        subtitles.enabled = false;
        subtitleBackground.enabled = false;
        running = false;

    }
}
