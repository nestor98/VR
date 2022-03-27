using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{


    [Range(0.0f, 5.0f)]
    public float rotationSpeed = 0.5f;


    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.5f;
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
        if (Input.GetMouseButtonDown(0)) // When clicked
            lastPos = Input.mousePosition; // Only update last pos to avoid jumping
        else if (Input.GetMouseButton(0)) // When dragging, move camera
        {
            var direction = Input.mousePosition - lastPos; // Direction between last frame and this one
            direction = new Vector3(-direction.y, direction.x, 0.0f);
            // Add to the euler angles instead of rotating the transform
            var euler = transform.eulerAngles + rotationSpeed * direction;
            lastPos = Input.mousePosition;

            // Clamp at poles
            euler.x = clampLatitude(euler.x);
            transform.eulerAngles = euler;
        }

    }
}
