using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float orbitRadius = 2, orbitSpeed = 1;
    float orbitAngle = 0;

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(orbitRadius, orbitSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        Vector3 position = new Vector3(Mathf.Cos(orbitAngle * Mathf.Deg2Rad), Mathf.Sin(orbitAngle * Mathf.Deg2Rad), 0) * radius + target.position;
        transform.position = position;

        // Increase angle
        orbitAngle += speed;

        // Full rotation has been made
        // Start back around at 0
        if(orbitAngle > 360) orbitAngle -= 360;
    }
}
