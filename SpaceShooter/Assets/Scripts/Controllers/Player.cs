using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

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
        PlayerMovement();
    }

    // Method to move the ship
    public void PlayerMovement()
    {
        // Initialize a direction vector
        Vector3 direction;

        // Left input
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Make sure the ship isn't moving in any other way or direction
            moveLeft = true;
            moveRight = false;
            decelerateLeft = false;
            decelerateRight = false;
        }
        // Decelerate if key is let go
        if (Input.GetKeyUp(KeyCode.LeftArrow)) decelerateLeft = true;

        // Move left
        if(moveLeft)
        {
            // Decelerating
            if(decelerateLeft)
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
        }

        // Right input
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Make sure the ship isn't moving in any other way or direction
            moveRight = true;
            moveLeft = false;
            decelerateRight = false;
            decelerateLeft = false;
        }
        // Decelerate if key is let go
        if (Input.GetKeyUp(KeyCode.RightArrow)) decelerateRight = true;

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
        }

        // Up input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Make sure the ship isn't moving in any other way or direction
            moveUp = true;
            moveDown = false;
            decelerateUp = false;
            decelerateDown = false;
        }
        // Decelerate if key is let go
        if (Input.GetKeyUp(KeyCode.UpArrow)) decelerateUp = true;

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
        }

        // Down input
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Make sure the ship isn't moving in any other way or direction
            moveDown = true;
            moveUp = false;
            decelerateDown = false;
            decelerateUp = false;
        }
        // Decelerate if key is let go
        if (Input.GetKeyUp(KeyCode.DownArrow)) decelerateDown = true;

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
        }
    }

}
