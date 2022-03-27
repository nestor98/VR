using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Disappearing : MonoBehaviour
{
    public float min_dist = 2.0f, 
                 max_dist = 12.0f;

    private MeshRenderer meshRenderer;

    private Camera cam;


    void Start()
    {
        cam = Camera.main;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position - transform.position).magnitude;

        float transparency = Mathf.InverseLerp(min_dist, max_dist, dist);

        // Calculate transparency depending on distance
        // You have to select the property name, not the display name
        meshRenderer.material.SetFloat("_Transparency", transparency);

        // Also disable the shadows if it is transparent enough:
        if (transparency < 0.85f)
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        else
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}