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

    void Start()
    {
        subtitles.enabled = false;
        subtitleBackground.enabled = false;
    }

    public void displaySubtitles()
    {
        StartCoroutine("Subtitles");
    }

    // reads subtitles from .txt files in Resources folder
    IEnumerator Subtitles()
    {
        subtitles.enabled = true;
        subtitleBackground.enabled = true;

        string currentRoom = GameObject.Find("Lore Note").GetComponent<InteractableLore>().roomName;
        
        fileName = currentRoom+"Subtitles";
        
        string[] linesArr = Resources.Load<TextAsset>(fileName).text.Split("\n"[0]);
        

        foreach(string line in linesArr)
        {
            subtitles.text = line.Split('@')[0];
            Debug.Log(subtitles.text);
            float subtitleDelay = float.Parse(line.Split('@')[1]);
            yield return new WaitForSeconds(subtitleDelay);
        }

        subtitles.enabled = false;
        subtitleBackground.enabled = false;

    }
}
