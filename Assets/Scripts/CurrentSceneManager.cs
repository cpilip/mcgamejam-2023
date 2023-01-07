using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    //Singleton for the MazeCreator
    private static CurrentSceneManager instance;

    public static CurrentSceneManager Instance { get { return instance; } }

    public int currentRoomIndex = 0;
    private List<string> roomSceneNames = new List<string>() {"Room1","Room2","Room3","Room4"};

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //Initialize CurrentSceneManager
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetNextScene()
    {
        currentRoomIndex++;
    }  

    public void SwapToNextScene()
    {
        Debug.Log("sceneName to load: " + roomSceneNames[currentRoomIndex]);
        SceneManager.LoadScene(roomSceneNames[currentRoomIndex]);
    } 

    public void SwapToMaze()
    {
        SceneManager.LoadScene("Maze");
    }
}
