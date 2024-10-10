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

    Color radarColor = Color.green;
    public float radarRange = 3;
    public int radarPoints = 5;

    public GameObject powerupPrefab;
    public float powerupRange = 2;
    public int numberOfPowerups = 6;
    List<GameObject> powerUps;

    public bool won = false;

    public bool boosting = false;
    public float speedMultiplier = 2, boostTime;
    float boostCountdown;
    public SpriteRenderer shipSprite;

    public bool shield = false;

    private void Start()
    {
        // Calculate acceleration
        acceleration = maxSpeed / accelerationTime;

        // Create a list of powerup prefabs
        powerUps = new List<GameObject>(numberOfPowerups);
        for(int i = 0; i < numberOfPowerups; i++)
        {
            powerUps.Insert(i, Instantiate(powerupPrefab));
        }

        // Set the boost countdown
        boostCountdown = boostTime;
    }

    void Update()
    {
        // Move the player
        PlayerMovement();

        // Call the method to place powerups as long as there are powerups to display
        if(numberOfPowerups != 0) SpawnPowerups(powerupRange, numberOfPowerups);

        // Spawn a bomb
        if(Input.GetKeyDown(KeyCode.Z)) Instantiate(bombPrefab, transform.position, Quaternion.identity);

        // Speed boost
        if(Input.GetKeyDown(KeyCode.X))
        {
            // Only boost if there's enough power ups and the player isn't currently boosting
            if(numberOfPowerups > 0 && !boosting)
            {
                // Use a power up
                Destroy(powerUps[numberOfPowerups - 1]);
                numberOfPowerups--;

                // Boost the speed
                boosting = true;
                maxSpeed *= speedMultiplier;

                // Change the ship colour
                shipSprite.color = Color.cyan;
            }
        }
        // Countdown the boost timer until speed should be reset
        if(boosting)
        {
            // Decrease the timer
            boostCountdown -= Time.deltaTime;

            if (boostCountdown <= 0)
            {
                // Reset the countdown
                boostCountdown = boostTime;

                // Reset speed and turn off the boost
                boosting = false;
                maxSpeed /= speedMultiplier;

                // Reset the ship colour
                shipSprite.color = Color.white;
            }
        }

        // Shield
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Only turn on the shield if there's enough power ups and a shield isn't currently active
            if(numberOfPowerups > 0 && !shield)
            {
                // Use a power up
                Destroy(powerUps[numberOfPowerups - 1]);
                numberOfPowerups--;

                // Activate the shield
                shield = true;
            }
        }

        // Spawn the shield as long as the enemy is still alive
        if (!won && shield) EnemyRadar(radarRange, radarPoints);
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

            // Get the translation
            Vector3 translation = horizontalVelocity * Time.deltaTime * direction;

            // Boost the speed
            if (boosting) translation *= speedMultiplier;

            // Move the position
            transform.position += translation;
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

            // Get the translation
            Vector3 translation = horizontalVelocity * Time.deltaTime * direction;

            // Boost the speed
            if (boosting) translation *= speedMultiplier;

            // Move the position
            transform.position += translation;
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

            // Get the translation
            Vector3 translation = verticalVelocity * Time.deltaTime * direction;

            // Boost the speed
            if (boosting) translation *= speedMultiplier;

            // Move the position
            transform.position += translation;
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

            // Get the translation
            Vector3 translation = verticalVelocity * Time.deltaTime * direction;

            // Boost the speed
            if (boosting) translation *= speedMultiplier;

            // Move the position
            transform.position += translation;
        }
    }

    // Method to create a radar around the player
    public void EnemyRadar(float radius, int circlePoints)
    {
        // Change the color of the radar if the enemy is within the radius
        float distance = Vector3.Distance(enemyTransform.position, transform.position);
        if (distance <= radius) radarColor = Color.red;
        else radarColor = Color.green;

        // Get the space between each circle point
        float angle = 360 / circlePoints;

        // Draw the radar
        for(int i = 0; i < circlePoints; i++)
        {
            // Initialize variables for the start and end of the line
            Vector3 start;
            Vector3 end;

            // If at the end of the points, connect the line to the start
            if(i == circlePoints - 1)
            {
                start = new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle * i * Mathf.Deg2Rad), 0) * radius + transform.position;
                end = new Vector3(Mathf.Cos(angle * 0 * Mathf.Deg2Rad), Mathf.Sin(angle * 0 * Mathf.Deg2Rad), 0) * radius + transform.position;
            }
            // Else draw from this point to the next
            else
            {
                start = new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle * i * Mathf.Deg2Rad), 0) * radius + transform.position;
                end = new Vector3(Mathf.Cos(angle * (i + 1) * Mathf.Deg2Rad), Mathf.Sin(angle * (i + 1) * Mathf.Deg2Rad), 0) * radius + transform.position;
            }
            
            Debug.DrawLine(start, end, radarColor);
        }
    }

    // Method to place power ups around the player
    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        // Get an equal angle spacer
        float angle = 360 / numberOfPowerups;

        // Place each powerup
        for(int i = 0; i < numberOfPowerups; i++)
        {
            // Place a powerup at the position
            Vector3 position = new Vector3(Mathf.Cos(angle * (i + 1) * Mathf.Deg2Rad), Mathf.Sin(angle * (i + 1) * Mathf.Deg2Rad), 0) * radius + transform.position;
            powerUps[i].transform.position = position;
        }
    }
}
