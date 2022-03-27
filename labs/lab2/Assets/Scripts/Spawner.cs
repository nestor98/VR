
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    // Properties that can be changed from the Inspetor tab
    public GameObject ItemToSpawn; // Item to be spawned
    public Vector3 center; // Center of the cube to spawn
    public Vector3 size; // Size of the cube to spawn
                         // Start is called before the first frame update

    // Maximum and current cooldown to control the spawning rate:
    public float max_cd = 1.0f;
    private float cooldown = 0.0f;

    // Camera and xz area of the spawner, to spawn only if the camera is in the area:
    private Camera cam;
    private Rect area;

    void Start()
    {
        cam = Camera.main;
        // XZ area of the spawner:
        area = new Rect(center.x - size.x / 2, center.z - size.z / 2, size.x, size.z);
    }
    // Update is called once per frame
    void Update()
    {
        // Reduce cooldown if needed:
        if (cooldown >= 0.0f) cooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Q) && cooldown < 0.0f && CameraInRange())
        {// When Q is pressed, an item is spawned
            SpawnItem();
            cooldown = max_cd;
        }
    }
    public void SpawnItem()
    {
        // Position to spawn
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                            Random.Range(-size.y / 2, size.y / 2),
                                            Random.Range(-size.z / 2, size.z / 2));
        // Instantiate the object
        Instantiate(ItemToSpawn, pos, Quaternion.Euler(0,Random.Range(0,360),0));
    }

    private bool CameraInRange()
    {
        return area.Contains(new Vector2(cam.transform.position.x, cam.transform.position.z));
    }

    // Debug funcion. In the Scene tab you can see a Red Box, which is the
    // volume where the object is going to be spawned.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}