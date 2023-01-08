using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class FollowCamera : MonoBehaviour
{
    void Start()
    {
        Camera _camera = Camera.main;
        if(SceneManager.GetActiveScene().name != "Maze")
        {
            _camera.GetComponent<CinemachineBrain>().enabled = false;
            Debug.Log("test");
        }
    }
}

