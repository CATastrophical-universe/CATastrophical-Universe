using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables to control movement
    private float horizontal; // Stores horizontal input
    private bool isRight = true; // Indicates if the player is facing right

    // References to components and objects
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component
    [SerializeField] private Transform groundCheck; // Reference to the ground check object
    [SerializeField] private LayerMask groundLayer; // Layer mask to define what is considered ground
    [SerializeField] private float jumpingPower = 16f; // Jumping force
    [SerializeField] private float speed = 10f; // Movement speed
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //speed = SettingsMenu.GetSpeed();
        // Get horizontal input
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if the jump button is pressed and the player is grounded
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            // Apply upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // If jump button is released and player is still going up, reduce the upward velocity
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Flip the player's direction if necessary
        Flip();
    }

    // FixedUpdate is called at a fixed interval
    private void FixedUpdate()
    {
        // Move the player horizontally based on input
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // Check if the player is grounded
    public bool IsGrounded()
    {
        // Use OverlapCircle to check if the groundCheck object overlaps with any objects in the groundLayer within a certain radius
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Flip the player's direction if moving in the opposite direction
    private void Flip()
    {
        if ((isRight && horizontal < 0f) || (!isRight && horizontal > 0f))
        {
            // Update the direction the player is facing
            isRight = !isRight;

            // Flip the player's local scale on the x-axis to change its direction
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

#if UNITY_INCLUDE_TESTS
    public void SetHorizontal(float horizontal) { this.horizontal = horizontal; }
#endif
}