using UnityEngine;
using System.Collections;
using Unity.Android.Types;
using System.Collections.Generic;
using static Codice.CM.Common.CmCallContext;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    private float acceleration;
    private float maxSpeed = 5f;
    private float accelerationTime = 5f;
    public float horizontalVelocity = 0, verticalVelocity = 0;

    public bool moveLeft = false, moveRight = false, moveUp = false, moveDown = false;
    public bool decelerateLeft = false, decelerateRight = false, decelerateUp = false, decelerateDown = false;

    private void Start()
    {
        // Calculate acceleration
        acceleration = maxSpeed / accelerationTime;
    }

    void Update()
    {
        EnemyMovement();
    }

    // Method to move the enemy
    public void EnemyMovement()
    {
        // Initialize a direction vector
        Vector3 direction;

        // Left movement
        // Don't move if decelerating to the right
        if (player.transform.position.x < transform.position.x && !decelerateRight)
        {
            // Make sure the ship isn't moving in any other way or direction
            moveLeft = true;
            moveRight = false;
            decelerateLeft = false;
            decelerateRight = false;
        }

        // Move left
        if (moveLeft)
        {
            // Decelerating
            if (decelerateLeft)
            {
                // Decrease speed
                if (horizontalVelocity > 0) horizontalVelocity -= acceleration * Time.deltaTime;
                // Stop if speed has reached 0
                else
                {
                    decelerateLeft = false;
                    horizontalVelocity = 0;
                    moveLeft = false;
                }
            }
            // Accelerating
            else
            {
                // Increase speed until maxed
                if (horizontalVelocity < maxSpeed) horizontalVelocity += acceleration * Time.deltaTime;
            }

            // Stop if hit the edge of the screen
            if (transform.position.x < -19.5)
            {
                // Stop movement and deceleration bools
                direction = Vector3.zero;
                horizontalVelocity = 0;
                decelerateLeft = false;
                moveLeft = false;
            }
            else direction = Vector3.left;

            // Move position
            transform.position += horizontalVelocity * Time.deltaTime * direction;

            // Check if passed the player position after moving left
            // Checking if moveLeft is true prevents deceleration from occuring after fully decelerated
            if (player.transform.position.x > transform.position.x && moveLeft) decelerateLeft = true;
        }

        // Right movement
        // Don't move if decelerating to the left
        if (player.transform.position.x > transform.position.x && !decelerateLeft)
        {
            // Make sure the ship isn't moving in any other way or direction
            moveRight = true;
            moveLeft = false;
            decelerateRight = false;
            decelerateLeft = false;
        }

        // Move right
        if (moveRight)
        {
            // Decelerating
            if (decelerateRight)
            {
                // Decrease speed
                if (horizontalVelocity > 0) horizontalVelocity -= acceleration * Time.deltaTime;
                // Stop if speed has reached 0
                else
                {
                    decelerateRight = false;
                    horizontalVelocity = 0;
                    moveRight = false;
                }
            }
            // Accelerating
            else
            {
                // Increase speed until maxed
                if (horizontalVelocity < maxSpeed) horizontalVelocity += acceleration * Time.deltaTime;
            }

            // Stop if hit the edge of the screen
            if (transform.position.x > 19.5)
            {
                // Stop movement and deceleration bools
                direction = Vector3.zero;
                horizontalVelocity = 0;
                decelerateRight = false;
                moveRight = false;
            }
            else direction = Vector3.right;

            // Move position
            transform.position += horizontalVelocity * Time.deltaTime * direction;

            // Check if passed the player position after moving right
            // Checking if moveRight is true prevents deceleration from occuring after fully decelerated
            if (player.transform.position.x < transform.position.x && moveRight) decelerateRight = true;
        }

        // Up movement
        // Don't move if decelerating down
        if (player.transform.position.y > transform.position.y && !decelerateDown)
        {
            // Make sure the ship isn't moving in any other way or direction
            moveUp = true;
            moveDown = false;
            decelerateUp = false;
            decelerateDown = false;
        }

        // Move up
        if (moveUp)
        {
            // Decelerating
            if (decelerateUp)
            {
                // Decrease speed
                if (verticalVelocity > 0) verticalVelocity -= acceleration * Time.deltaTime;
                // Stop if speed has reached 0
                else
                {
                    decelerateUp = false;
                    verticalVelocity = 0;
                    moveUp = false;
                }
            }
            // Accelerating
            else
            {
                // Increase speed until maxed
                if (verticalVelocity < maxSpeed) verticalVelocity += acceleration * Time.deltaTime;
            }

            // Stop if hit the edge of the screen
            if (transform.position.y > 9.5)
            {
                // Stop movement and deceleration bools
                direction = Vector3.zero;
                verticalVelocity = 0;
                decelerateUp = false;
                moveUp = false;
            }
            else direction = Vector3.up;

            // Move position
            transform.position += verticalVelocity * Time.deltaTime * direction;

            // Check if passed the player position after moving up
            // Checking if moveUp is true prevents deceleration from occuring after fully decelerated
            if (player.transform.position.y < transform.position.y && moveUp) decelerateUp = true;
        }

        // Down movement
        // Don't move if decelerating down
        if (player.transform.position.y < transform.position.y && !decelerateUp)
        {
            // Make sure the ship isn't moving in any other way or direction
            moveDown = true;
            moveUp = false;
            decelerateDown = false;
            decelerateUp = false;
        }

        // Move down
        if (moveDown)
        {
            // Decelerating
            if (decelerateDown)
            {
                // Decrease speed
                if (verticalVelocity > 0) verticalVelocity -= acceleration * Time.deltaTime;
                // Stop if speed has reached 0
                else
                {
                    decelerateDown = false;
                    verticalVelocity = 0;
                    moveDown = false;
                }
            }
            // Accelerating
            else
            {
                // Increase speed until maxed
                if (verticalVelocity < maxSpeed) verticalVelocity += acceleration * Time.deltaTime;
            }

            // Stop if hit the edge of the screen
            if (transform.position.y < -9.5)
            {
                // Stop movement and deceleration bools
                direction = Vector3.zero;
                verticalVelocity = 0;
                decelerateDown = false;
                moveDown = false;
            }
            else direction = Vector3.down;

            // Move position
            transform.position += verticalVelocity * Time.deltaTime * direction;

            // Check if passed the player position after moving down
            // Checking if moveDown is true prevents deceleration from occuring after fully decelerated
            if (player.transform.position.y > transform.position.y && moveDown) decelerateDown = true;
        }
    }

}
