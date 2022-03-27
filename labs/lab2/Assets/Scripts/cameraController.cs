using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float rotSpeed = 20.0f;
    public float moveSpeed = 40.0f;


    public List<GameObject> rooms;

    // OT-0:
    public string hidingTag; // Tag of the objects to hide
    // To restore previously hidden objects:
    private HashSet<GameObject> hiddenObjs = new HashSet<GameObject>();


    private int currentRoom = 1;


    // Start is called before the first frame update
    void Start()
    {
        UpdateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyInputs();
        if (currentRoom == 1) RayCast(); // for OT-0

        if (Input.GetKeyDown("space")) {
            Cursor.visible = !Cursor.visible;
        }

        RoomsHack(); // Disable rooms for performance (I just realized pointlights are bad)
    }

    private void ApplyInputs()
    {
        // Rotation input:
        float y = Input.GetAxis("Mouse X"),
              x = -Input.GetAxis("Mouse Y");

        Vector3 euler = new Vector3(x, y, 0) * rotSpeed * Time.deltaTime;
        euler += transform.eulerAngles;

        // Movement input:
        Vector3 movement = MovementInputs();
        if (movement.sqrMagnitude > 1e-6)
        {
            transform.eulerAngles = new Vector3(0, euler.y, 0); // Only move in xz plane
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        // Restore latitude
        transform.eulerAngles = euler;
    }
    
    // Returns the Vector3 with the movement inputs
    private Vector3 MovementInputs()
    {
        float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");
        return new Vector3(x, 0, z);
    }

    private void RayCast()
    {
        // Show any previously hidden objects:
        foreach (var obj in hiddenObjs)
        {
            obj.SetActive(true);
        }
        hiddenObjs.Clear();

        // Ray cast at the center of the screen:
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        foreach (var hit in hits)
        { // Check hits
            var obj = hit.transform.gameObject;
            if (obj.CompareTag(hidingTag)) // If it matches the hiding tag
            {
                obj.SetActive(false); // Hide it
                hiddenObjs.Add(obj); // Save it to revert later
            }
        }
    }

    // Checks if a number from 1 to 4 has been pressed, and if so, switches to that room
    // and disables the rest
    private void RoomsHack()
    {
        int prev = currentRoom;
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentRoom = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentRoom = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentRoom = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) currentRoom = 4;

        // if changed, update:
        if (prev != currentRoom) UpdateRooms();
    }

    private void UpdateRooms()
    {
        for (int i = 0; i<rooms.Count; i++)
        {
            if (i+1 != currentRoom)
            {
                rooms[i].SetActive(false);
            }
            else
            {
                rooms[i].SetActive(true);
                Vector3 roomPos = rooms[i].transform.position;
                transform.position = new Vector3(roomPos.x, transform.position.y, roomPos.z); // teleport to that room (keep y)
            }
        }
    }

}
