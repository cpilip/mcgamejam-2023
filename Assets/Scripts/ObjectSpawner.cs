using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 pos = new Vector3 (1.5f, 1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectToSpawn); 
    }
}
