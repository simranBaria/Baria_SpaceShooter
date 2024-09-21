using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    public Vector3 currentStar, nextStar;
    public bool drawing = false;

    // Update is called once per frame
    void Update()
    {
        // Do not call if already drawing
        if(!drawing)
        {
            // Start drawing
            drawing = true;
            DrawConstellation();
        }
    }

    // Method to draw the constellation
    public void DrawConstellation()
    {
        StartCoroutine(Draw());
    }

    // Coroutine to draw the lines between stars
    IEnumerator Draw()
    {
        // Loop from the first star to the last
        for (int i = 0; i < starTransforms.Count - 1; i++)
        {
            // Get the starting star and the one to draw a line to
            currentStar = starTransforms[i].position;
            nextStar = starTransforms[i + 1].position;

            // Draw the line
            float time = 0;
            while (time <= drawingTime)
            {
                // Lerp to draw the line gradually
                Vector3 line = Vector3.Lerp(currentStar, nextStar, time * 2);
                Debug.DrawLine(currentStar, line, Color.white, 0.1f);
                time += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        // Done drawing
        StopCoroutine(Draw());
        drawing = false;
    }
}
