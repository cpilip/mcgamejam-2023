using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.5f;
    public float dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    public bool isDashing;

    void Start() {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (!RatAnimator.Instance.GetIsRunning())
            {
                RatAnimator.Instance.SetIsRunning(true);
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            RatAnimator.Instance.SetIsRunning(false);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb.velocity = movement * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0) // dash is over
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            FindObjectOfType<RatAnimator>().TrySqueak();
            int squeakInt = Random.Range(1,8);
            string squeakNum = squeakInt.ToString();
            FindObjectOfType<AudioManagerScript>().Play("Squeak"+squeakNum);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Vent")
        {
            Debug.Log("Entered Vent");
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(isDashing)
        {

        }
    }


    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.tag == "Laser");
    //     {
    //         Scene
    //     }
    // }
}
