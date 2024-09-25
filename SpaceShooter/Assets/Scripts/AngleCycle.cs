using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCycle : MonoBehaviour
{
    public List<float> angles;

    int current = 0;
    public float radius = 1;
    public Vector3 offset;

    public float time = 0;
    public float duration = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Stay on the current angle for a certain duration
        if(time <= duration)
        {
            // Convert the angles to radians
            Vector3 startPoint = Vector3.zero + offset;
            Vector3 endPoint = new Vector3(Mathf.Cos(angles[current] * Mathf.Deg2Rad), Mathf.Sin(angles[current] * Mathf.Deg2Rad), 0) * radius + offset;

            // Draw the line
            Debug.DrawLine(startPoint, endPoint, Color.blue);

            // Increment time
            time += Time.deltaTime;
        }
        else
        {
            // Reset time
            time = 0;

            // If hit the end of the list loop back to the start
            if (current + 1 == angles.Count) current = 0;
            else current++;
        }
    }
}
