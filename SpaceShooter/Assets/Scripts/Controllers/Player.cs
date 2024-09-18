using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public float acceleration;
    private float maxSpeed = 5f;
    private float accelerationTime = 5f;

    private void Start()
    {

    }
    void Update()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        // Reset the veocity to default speed
        Vector3 horizontalVelocity = Vector3.left * Time.deltaTime;
        Vector3 verticalVelocity = Vector3.up * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Stop moving if hit the edge of the screen
            if (transform.position.x < -19.5) horizontalVelocity = Vector3.zero;

            // Calculate acceleration
            acceleration = maxSpeed / accelerationTime;
            if (acceleration < maxSpeed) accelerationTime -= Time.deltaTime;

            transform.position += horizontalVelocity * acceleration;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Stop moving if hit the edge of the screen
            if (transform.position.x > 19.5) horizontalVelocity = Vector3.zero;

            // Calculate acceleration
            acceleration = maxSpeed / accelerationTime;
            if (acceleration < maxSpeed) accelerationTime -= Time.deltaTime;

            transform.position -= horizontalVelocity * acceleration;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Stop moving if hit the edge of the screen
            if (transform.position.y > 9.5) verticalVelocity = Vector3.zero;

            // Calculate acceleration
            acceleration = maxSpeed / accelerationTime;
            if (acceleration < maxSpeed) accelerationTime -= Time.deltaTime;

            transform.position += verticalVelocity * acceleration;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Stop moving if hit the edge of the screen
            if (transform.position.y < -9.5) verticalVelocity = Vector3.zero;

            // Calculate acceleration
            acceleration = maxSpeed / accelerationTime;
            if (acceleration < maxSpeed) accelerationTime -= Time.deltaTime;

            transform.position -= verticalVelocity * acceleration;
        }

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            acceleration = 0;
            accelerationTime = 5;
        }
    }

}
