using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum VentID
{
    RoomVent,
    MazeVent
}

public class DashableVent : Dashable
{
    [SerializeField] private VentID m_thisVent;

    public override void DashThrough()
    {
        Debug.Log("Go to next" + this.gameObject.name);
        
        if (m_thisVent == VentID.RoomVent)
        {
            CurrentSceneManager.Instance.SwapToMaze();
        }
        else if (m_thisVent == VentID.MazeVent)
        {
            CurrentSceneManager.Instance.SwapToNextScene();
        }
    }
}
