using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    [SerializeField] private GameObject m_current;
    [SerializeField] private GameObject m_currentWirebox;

    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.parent);
        m_current = null;
    }

    void Update()
    {
        if (Input.GetButtonDown("interact") && m_current)
        {
            m_current.SendMessage("InteractWith");
            RatAnimator.Instance.TryInteract();
            StartCoroutine(LetAnimationPlay());

        }

        // Fix for re-entering wireboxes - to not introduce more bugs, we'll have to leave
        // m_current as is, but we know it's null when entering a wirebox since the tag is Dashable,
        // not Interactable; we'll exploit this as a flag
        if (Input.GetButtonDown("interact") && m_current == null && m_currentWirebox && m_currentWirebox.tag == "Interactable")
        {
            m_currentWirebox.SendMessage("InteractWith");
            RatAnimator.Instance.TryInteract();
            StartCoroutine(LetAnimationPlay());
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Entered " + other.tag);
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("Setting m_current");
            m_current = other.gameObject;

        }

        // Fix for re-entering wireboxes - we'll secretly track the box when it still has the Dashable 
        // tag in the CharacterInteractor
        if (other.CompareTag("Dashable") && other.gameObject.name.Contains("DashableWiresBoxTrigger"))
        {
            Debug.Log("Setting wirebox");
            m_currentWirebox = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.gameObject.Equals(m_current))
            {
                m_current = null;
            }
        }

        // Fix for re-entering wireboxes
        if (other.CompareTag("Interactable") && other.gameObject.name.Contains("DashableWiresBoxTrigger"))
        {
            if (other.gameObject.Equals(m_currentWirebox))
            {
                Debug.Log("Setting wirebox");
                m_currentWirebox = null;
            }
        }
    }

    IEnumerator LetAnimationPlay()
    {
        this.gameObject.GetComponent<RatMovement>().isLocked = true;
        yield return new WaitForSeconds(1.75f);
        this.gameObject.GetComponent<RatMovement>().isLocked = false;
    }
}
