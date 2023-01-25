using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimator : MonoBehaviour
{
    private static RatAnimator _instance;

    public static RatAnimator Instance { get { return _instance; } }

    private bool animatorInitialized = false;
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
    public void TryLookAround()
    {
        anim.SetTrigger("LookAround");
    }

    public void TryTransform()
    {
        anim.SetTrigger("Transform");
    }


    public IEnumerator TryWakeup()
    {
        anim.SetTrigger("Wakeup");
        yield return new WaitForSeconds(1.5f);
        animatorInitialized = true;
    }

    public bool GetAnimatorInitialized()
    {
        return animatorInitialized;
    }

    public void Update()
    {
        if (Input.anyKey && animatorInitialized == false)
        {
            StartCoroutine(TryWakeup());
        }
    }
}
