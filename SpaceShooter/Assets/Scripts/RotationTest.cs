using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public float angularSpeed = 5, targetAngle = 180;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Draw a line from the object to the target
        Debug.DrawLine(transform.position, target.position, Color.blue);

        // Set the start and end positions of the line
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.up;

        // Draw the line
        Debug.DrawLine(startPosition, endPosition, Color.red);

        // Since the up line is at 90 degrees, we need to subtract the target by 90 
        targetAngle = (Mathf.Atan2(target.position.y, target.position.x) * Mathf.Rad2Deg) - 90;

        // Rotate the angle until the target has been reached
        if (transform.eulerAngles.z < targetAngle) transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
        else if (transform.eulerAngles.z > targetAngle) transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);
    }

    // Method to convert an angle to in between -180 and 180
    public float StandardizeAngle(float inAngle)
    {
        inAngle = inAngle % 360;

        inAngle = (inAngle + 360) % 360;

        if (inAngle > 180) inAngle -= 360;

        return inAngle;
    }
}
