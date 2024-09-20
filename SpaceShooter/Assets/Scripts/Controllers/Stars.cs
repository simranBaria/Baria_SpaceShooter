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

    // Coroutine to draw the line between two stars
    IEnumerator Draw()
    {
        // Loop from the first star to the last
        for (int i = 0; i < starTransforms.Count - 1; i++)
        {
            currentStar = starTransforms[i].position;
            nextStar = starTransforms[i + 1].position;

            // Draw the line
            for (int j = 10; j > 0; j--)
            {
                Vector3 line = currentStar + (nextStar - currentStar) / j;
                Debug.DrawLine(currentStar, line, Color.white, drawingTime / 10);
                yield return new WaitForSeconds(drawingTime / 10);
            }
        }
        StopCoroutine(Draw());
    }
}
