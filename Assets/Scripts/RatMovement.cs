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

    private Transform spritePrefabObject;
    private Transform lightPrefabObject;
    private bool facingRight = true;
    public bool isDashing;
    public bool isLocked = false;

    void Awake()
    {
        // Turns out an animator component overrides any transform changes via script
        // This script is on the Rat prefab > get the RatSprite, not the SideRat
        spritePrefabObject = this.transform.Find("RatSprite");
        lightPrefabObject = this.transform.GetChild(0);
        Debug.Log(spritePrefabObject.gameObject.name);
    }

    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (RatAnimator.Instance.GetAnimatorInitialized() && !isLocked)
        {
            // Input coming in, set animator to running
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (!RatAnimator.Instance.GetIsRunning())
                {
                    RatAnimator.Instance.SetIsRunning(true);
                    FindObjectOfType<AudioManagerScript>().Play("Walk");
                }
            }

            // No input, so stop running
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                RatAnimator.Instance.SetIsRunning(false);
                FindObjectOfType<AudioManagerScript>().Stop("Walk");
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movement.Normalize();

            rb.velocity = movement * activeMoveSpeed;

            // Moving right
            if (rb.velocity.x > 0 && !facingRight)
            {
                facingRight = !facingRight;
                // Sprite
                Vector3 rPosition = spritePrefabObject.localPosition;
                Vector3 rScale = spritePrefabObject.localScale;
                rPosition.x *= -1;
                rScale.x *= -1;
                spritePrefabObject.localPosition = rPosition;
                spritePrefabObject.localScale = rScale;
                // Light
                rPosition = lightPrefabObject.localPosition;
                rScale = lightPrefabObject.localScale;
                rPosition.x *= -1;
                rScale.x *= -1;                
                lightPrefabObject.localPosition = rPosition;
                lightPrefabObject.localScale = rScale;
                
            }
            // Moving left
            else if (rb.velocity.x < 0 && facingRight)
            {
                facingRight = !facingRight;
                // Sprite
                Vector3 rPosition = spritePrefabObject.localPosition;
                Vector3 rScale = spritePrefabObject.localScale;
                rPosition.x *= -1;
                rScale.x *= -1;
                spritePrefabObject.localPosition = rPosition;
                spritePrefabObject.localScale = rScale;
                // Light
                rPosition = lightPrefabObject.localPosition;
                rScale = lightPrefabObject.localScale;
                rPosition.x *= -1;
                rScale.x *= -1;                
                lightPrefabObject.localPosition = rPosition;
                lightPrefabObject.localScale = rScale;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    isDashing = true;
                    FindObjectOfType<AudioManagerScript>().Play("Dash");
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
                StartCoroutine(LetAnimationPlay());
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator LetAnimationPlay()
    {
        yield return new WaitForSeconds(0.8f);
        int squeakInt = Random.Range(1, 8);
        string squeakNum = squeakInt.ToString();
        FindObjectOfType<AudioManagerScript>().Play("Squeak" + squeakNum);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vent")
        {

            Debug.Log("Entered Vent");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isDashing)
        {

        }
    }
}