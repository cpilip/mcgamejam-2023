using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    private static CameraChanger _instance;

    public static CameraChanger Instance { get { return _instance; } }

    [SerializeField] private CinemachineVirtualCamera _mainFollowCam, _cutsceneCam;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    public void SwapToCutsceneCamera()
    {
        _cutsceneCam.m_Lens.OrthographicSize = 3;
        _cutsceneCam.Priority = _mainFollowCam.Priority + 2;
    }

    
}
