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
        // else if m_current && character is NOT  in dash state && m_current.gameObject.name.contains(`JumpOver`);
        /*else if (m_current && this.CompareTag("Walking")) {
            m_current.SendMessage("NotDashingAnymore")
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dashable"))
        {
            Debug.Log("Passing through object " + other.gameObject);
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
