using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;


public class latitudescript : MonoBehaviour
{
    public Text latitudeui;

    [SerializeField] bool showEulerDirectly;

    // Start is called before the first frame update
    void Start()
    {
        latitudeui.text = "hmm\notra linea"; showEulerDirectly = false;
    }

    double toLatitude(double eulerAngle)
    {
        double lat = 360.0 - eulerAngle % 360.0; // Flip and use mod to cap it
        if (lat > 180) return -(360.0-lat); // Negative latitudes
        return lat;
    }

    double toLongitude(double eulerAngle)
    {
        double lon = eulerAngle % 360.0; // cap, just in case
        if (lon > 180) return -(360.0 - lon); // negative longitudes 
        return lon;
    }

    // Update is called once per frame
    void Update()
    {
        if (showEulerDirectly)
        {
            latitudeui.text = "latitude: " + Camera.main.transform.localEulerAngles.x +
                          "\nlongitude: " + Camera.main.transform.localEulerAngles.y;
        }
        else
        {
            latitudeui.text = "latitude: " + toLatitude(Camera.main.transform.localEulerAngles.x) +
                              "\nlongitude: " + toLongitude(Camera.main.transform.localEulerAngles.y);
        }
    }
}
