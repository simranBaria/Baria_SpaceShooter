using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float sightDistance, visionAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Draw a cone from 0 to the angle
        for(int i = 0; i <= visionAngle; i++)
        {
            Debug.DrawLine(Vector3.zero, new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad), 0) * sightDistance, Color.green);
        }
    }
}
