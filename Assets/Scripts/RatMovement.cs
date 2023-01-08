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

    public float dashLength = 0.2f;
    public float dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    public bool isDashing;
    private bool facingRight = true;

    void Start() {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (RatAnimator.Instance.GetInitialized())
        {


            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                if (!RatAnimator.Instance.GetIsRunning())
                {
                    RatAnimator.Instance.SetIsRunning(true);
                    FindObjectOfType<AudioManagerScript>().Play("Walk");
                }
            }


        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            RatAnimator.Instance.SetIsRunning(false);
            FindObjectOfType<AudioManagerScript>().Stop("Walk");
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
                FindObjectOfType<AudioManagerScript>().Play("Dash");
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movement.Normalize();

            rb.velocity = movement * activeMoveSpeed;

            if (rb.velocity.x > 0 && !facingRight)
            {
                facingRight = !facingRight;
                Vector3 theScale = this.transform.GetChild(0).localScale;
                theScale.x *= -1;
                this.transform.GetChild(0).localScale = theScale;
            }
            else if (rb.velocity.x < 0 && facingRight)
            {
                facingRight = !facingRight;
                Vector3 theScale = this.transform.GetChild(0).localScale;
                theScale.x *= -1;
                this.transform.GetChild(0).localScale = theScale;
            }

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