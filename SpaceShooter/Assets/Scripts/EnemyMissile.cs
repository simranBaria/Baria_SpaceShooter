using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    GameObject enemy, player;
    public float directionAngle, maxSpeed, speedTime, acceleration, velocity;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy and player game objects
        enemy = GameObject.Find("Enemy");
        player = GameObject.Find("Player");

        // Get the direction angle to move in based on enemy direction
        if (enemy.GetComponent<Enemy>().moveLeft)
        {
            if (enemy.GetComponent<Enemy>().moveUp) directionAngle = 135;
            else if (enemy.GetComponent<Enemy>().moveDown) directionAngle = 225;
            else directionAngle = 180;
        }
        else if (enemy.GetComponent<Enemy>().moveRight)
        {
            if (enemy.GetComponent<Enemy>().moveUp) directionAngle = 45;
            else if (enemy.GetComponent<Enemy>().moveDown) directionAngle = 315;
            else directionAngle = 0;
        }
        else if (enemy.GetComponent<Enemy>().moveUp) directionAngle = 90;
        else if (enemy.GetComponent<Enemy>().moveDown) directionAngle = 270;
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

        // Check if the player has a shield
        if(player.GetComponent<Player>().shield)
        {
            // Get deflected by the shield if hit
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance <= player.GetComponent<Player>().radarRange)
            {
                // Deflect the missile by changing it's direction
                directionAngle -= 180;

                // Recalculate the direction vector
                direction = new Vector3(Mathf.Cos(directionAngle * Mathf.Deg2Rad), Mathf.Sin(directionAngle * Mathf.Deg2Rad), 0);

                // Tell the player to deactivate the shield
                player.GetComponent<Player>().shield = false;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't destroy the enemy
        if (collision.gameObject != enemy)
        {
            // If the enemy was killed, tell the player that they won
            if (collision.gameObject == player) enemy.GetComponent<Enemy>().won = true;

            // Destroy object and self
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
