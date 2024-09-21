using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    public Vector3 destination;
    public bool horizontalDone = false, verticalDone = false;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a destination to start
        destination = GenerateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    // Method to move the asteroid
    public void AsteroidMovement()
    {
        // Move horizontally if not finished moving horizontally
        if(!horizontalDone)
        {
            // Moveleft
            if (destination.x + arrivalDistance <= transform.position.x) transform.position += moveSpeed * Time.deltaTime * Vector3.left;
            // Move right
            else if (destination.x - arrivalDistance >= transform.position.x) transform.position += moveSpeed * Time.deltaTime * Vector3.right;
            // Done moving
            else horizontalDone = true;
        }

        // Move veritcally if not finished moving vertically
        if (!horizontalDone)
        {
            // Move up
            if (destination.y + arrivalDistance <= transform.position.y) transform.position += moveSpeed * Time.deltaTime * Vector3.up;
            // Move down
            else if (destination.y - arrivalDistance >= transform.position.y) transform.position += moveSpeed * Time.deltaTime * Vector3.down;
            // Done moving
            else verticalDone = true;
        }

        // Check if finished moving
        if(horizontalDone && verticalDone)
        {
            // Get a new destination and start moving towards it
            horizontalDone = false;
            verticalDone = false;
            destination = GenerateDestination();
        }
    }

    // Method to generate a destination for the asteroid
    public Vector3 GenerateDestination()
    {
        // Generate a random point within range
        float x = transform.position.x + Random.Range(-maxFloatDistance, maxFloatDistance);
        float y = transform.position.y + Random.Range(-maxFloatDistance, maxFloatDistance);

        // Return vector with generated points
        return new Vector3(x, y, 0);
    }
}
