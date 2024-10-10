using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    GameObject player;
    public float directionAngle, maxSpeed, speedTime, acceleration, velocity;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player game object
        player = GameObject.Find("Player");

        // Get the direction angle to move in based on player direction
        if (player.GetComponent<Player>().moveLeft)
        {
            if (player.GetComponent<Player>().moveUp) directionAngle = 135;
            else if (player.GetComponent<Player>().moveDown) directionAngle = 225;
            else directionAngle = 180;
        }
        else if (player.GetComponent<Player>().moveRight)
        {
            if (player.GetComponent<Player>().moveUp) directionAngle = 45;
            else if (player.GetComponent<Player>().moveDown) directionAngle = 315;
            else directionAngle = 0;
        }
        else if (player.GetComponent<Player>().moveUp) directionAngle = 90;
        else if (player.GetComponent<Player>().moveDown) directionAngle = 270;
        // Direction angle will default to straight up if no movement is occuring
        else directionAngle = 90;

        // Create the direction vector
        direction = new Vector3(Mathf.Cos(directionAngle * Mathf.Deg2Rad), Mathf.Sin(directionAngle * Mathf.Deg2Rad), 0);

        // Calculate acceleration
        acceleration = maxSpeed / speedTime;
        velocity = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bomb
        transform.position += velocity * Time.deltaTime * direction;

        // Decelerate speed
        velocity -= acceleration * Time.deltaTime;

        // Destroy self if stopped
        if (velocity <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't destroy the player
        if (collision.gameObject != player)
        {
            // If the enemy was killed, tell the player that they won
            if (collision.gameObject == GameObject.Find("Enemy")) player.GetComponent<Player>().won = true;

            // Destroy object and self
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
