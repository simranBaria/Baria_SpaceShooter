using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public float redAngle, blueAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the vectors from the angles
        Vector3 redPoint = new Vector3(Mathf.Cos(redAngle * Mathf.Deg2Rad), Mathf.Sin(redAngle * Mathf.Deg2Rad), 0);
        Vector3 bluePoint = new Vector3(Mathf.Cos(blueAngle * Mathf.Deg2Rad), Mathf.Sin(blueAngle * Mathf.Deg2Rad), 0);

        // Display the lines with length 1
        Debug.DrawLine(Vector3.zero, Vector3.Normalize(redPoint), Color.red);
        Debug.DrawLine(Vector3.zero, Vector3.Normalize(bluePoint), Color.blue);

        // Calculate the dot product between the two vectors
        if (Input.GetKeyDown(KeyCode.Space)) print("Dot product: " + (redPoint.x * bluePoint.x + redPoint.y * bluePoint.y));
    }
}
