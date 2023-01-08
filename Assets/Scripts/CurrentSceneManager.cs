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
    private List<Vector3> roomSpawnLocations = new List<Vector3>() {
        new Vector3(6.21f, -3.76f, -1f),
        new Vector3(-4.36f, -1.8f, -1f),
        new Vector3(-2.373097f, 0.1857041f, -1f),
        new Vector3(-4.33f, 2.32f, -1f)};

    [SerializeField] private GameObject player;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

   

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Room1")
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        if (scene.name != "TitlePage")
        {
            player.transform.position = roomSpawnLocations[currentRoomIndex];
        }
    }
}
