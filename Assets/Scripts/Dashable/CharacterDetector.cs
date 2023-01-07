using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private GameObject m_current;

    void Awake()
    {
        m_current = null;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Make sure character is in dash state
        if (m_current)
        {
            m_current.SendMessage("DashThrough");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dashable"))
        {
            Debug.Log("Hit wall at object " + other.gameObject);
            m_current = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dashable"))
        {
            if (other.gameObject.Equals(m_current))
            {
                m_current = null;
            }
        }
    }
}
