using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        new Vector3(-5.5f, -1.8f, -1f),
        new Vector3(-5.5f, 3.25f, -1f),
        new Vector3(-5.5f, -0.3f, -1f)};
    private int numCheese = 0;
    private TextMeshProUGUI cheeseAmountText;

    [SerializeField] private bool camEffects = true;
    [SerializeField] private bool camOverlay = true;
    private GameObject camOverlayObject;

    [SerializeField] private GameObject player;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CollectCheese()
    {
        numCheese++;
        cheeseAmountText.text = numCheese + "";
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
            camOverlayObject = GameObject.FindGameObjectWithTag("UI_CCTV");
            cheeseAmountText = GameObject.FindGameObjectsWithTag("UI_Cheese")[0].transform.Find("CheeseText").GetComponent<TextMeshProUGUI>();
            CheckCamEffects();
            CheckCamOverlay();
        }

        if (scene.name == "Room4")
        {
            player.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (scene.name != "TitlePage")
        {
            player.transform.position = roomSpawnLocations[currentRoomIndex];
        }
    }

    public void HideUI()
    {
        cheeseAmountText.transform.parent.parent.gameObject.SetActive(false);
        player.SetActive(false);
    }

    public void CheckCamEffects()
    {
        Camera.main.GetComponent<CRTPostEffecter>().enabled = camEffects;
    }

    public void CheckCamOverlay()
    {
        camOverlayObject.SetActive(camOverlay);
    }

    public void ToggleCamEffects()
    {
        camEffects = !camEffects;
    }

    public void ToggleCamOverlay()
    {
        camOverlay = !camOverlay;
    }

    public void ResetRat()
    {
        player.transform.position = roomSpawnLocations[currentRoomIndex];
        FindObjectOfType<AudioManagerScript>().Play("Laser");
    }

    void Update()
    {
        if (Input.GetKey("escape")) 
        {
            Application.Quit();
        }

        if (SceneManager.GetActiveScene().name != "TitlePage")
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleCamEffects();
                CheckCamEffects();

            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToggleCamOverlay();
                CheckCamOverlay();
            }
        }
    }
}
