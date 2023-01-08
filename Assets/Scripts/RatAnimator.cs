using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimator : MonoBehaviour
{
    private static RatAnimator instance = null;
    private bool initialized = false;
    [SerializeField] private Animator anim;
    private RatAnimator()
    {
    }

    public static RatAnimator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RatAnimator();
            }
            return instance;
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
        if (Input.anyKey && !initialized)
        {
            initialized = true;
            TryWakeup();
        }
    }
}
