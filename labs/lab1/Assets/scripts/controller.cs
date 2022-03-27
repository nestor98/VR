using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

//using UnityEngine.CoreModule.Mathf; // Clamp

public class controller : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void oldUpdate()
    {

        var direction = new Vector3(-Input.GetAxis("Vertical"),
                                    Input.GetAxis("Horizontal"), 0.0f);

        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }


    private double toLatitude(double eulerAngle)
    {
        double lat = 360.0 - eulerAngle % 360.0; // Flip and use mod to cap it
        if (lat > 180) return -(360.0 - lat); // Negative latitudes
        return lat;
    }

    // Doesnt allow to go beyond +90 and -90
    private float clampLatitude(float euler)
    {
        if (euler > 180.0f) return Mathf.Clamp(euler, 270.0f, 360.0f);
        else return Mathf.Min(euler, 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(-Input.GetAxis("Vertical"),
                                    Input.GetAxis("Horizontal"), 0.0f);
        // Add to the euler angles instead of rotating the transform
        var euler = transform.eulerAngles + direction;

        // Clamp at poles
        euler.x = clampLatitude(euler.x);

        transform.eulerAngles = euler;

    }
}
