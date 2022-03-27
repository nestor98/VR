using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCrystal : MonoBehaviour
{
    public GameObject spotLight; // points to this
    public float rotSpeed = 100.0f; // Rotation speed

    public float min_dist = 2.5f, max_dist = 18.0f;

    public Vector3 center; // Center of the cube to teleport
    public Vector3 size; // Size of the cube to teleport

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spotLight.transform.forward = transform.position - spotLight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to cam:
        float dist = (cam.transform.position - transform.position).magnitude;
        // Rotation speed based on distance:
        float speed = Mathf.InverseLerp(max_dist, min_dist, dist);
        transform.Rotate(0,speed * rotSpeed * Time.deltaTime, 0);
        // If close enough, teleport
        if (dist < min_dist) Teleport();
    }

    private void Teleport()
    {
        // Teleport in the cube:
        transform.position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                            Random.Range(-size.y / 2, size.y / 2),
                                            Random.Range(-size.z / 2, size.z / 2));

        // Turn spotlight
        spotLight.transform.forward = transform.position - spotLight.transform.position;
    }

    // Debug funcion. In the Scene tab you can see a Red Box, which is the
    // volume where the object is going to be spawned.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
