using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{

    [Range(0.0f, 1000.0f)]
    public float rotationSpeed = 200.0f;


    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        
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
        
        var direction = rotationSpeed * (Input.mousePosition - lastPos);
        //var direction = new Vector3(direction.x, direction.y, 0.0f);
        // Add to the euler angles instead of rotating the transform
        var euler = transform.eulerAngles + direction;
        lastPos = Input.mousePosition;

        // Clamp at poles
        euler.x = clampLatitude(euler.x);

        transform.eulerAngles = euler;

    }
}
