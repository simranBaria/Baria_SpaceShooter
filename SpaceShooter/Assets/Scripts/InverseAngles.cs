using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseAngles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Cos(45) = " + Mathf.Cos(45 * Mathf.Deg2Rad));
        print("Cos(-45) = " + Mathf.Cos(-45 * Mathf.Deg2Rad));
        print("Acos(Cos(45)) = " + Mathf.Acos(Mathf.Cos(45 * Mathf.Deg2Rad)));
        print("Acos(Cos(-45)) = " + Mathf.Acos(Mathf.Cos(-45 * Mathf.Deg2Rad)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
