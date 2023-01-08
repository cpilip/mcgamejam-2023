using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimator : MonoBehaviour
{
    private static RatAnimator _instance;

    public static RatAnimator Instance { get { return _instance; } }

    private bool initialized = false;
    [SerializeField] private Animator anim;

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

    public bool GetIsRunning()
    {
        return anim.GetBool("isRunning");
    }

    public void SetIsRunning(bool value)
    {
        anim.SetBool("isRunning", value);
    }

    public void TrySqueak()
    {
        anim.SetTrigger("Squeak");
    }

    public void TryInteract()
    {
        anim.SetTrigger("Interact");
    }

    public void TryNibble()
    {
        anim.SetTrigger("Nibble");
    }

    public void TryWakeup()
    {
        anim.SetTrigger("Wakeup");
    }

    public void Update()
    {
        if (Input.anyKey && initialized == false)
        {
            initialized = true;
            TryWakeup();
        }
    }
}
