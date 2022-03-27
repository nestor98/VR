using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTexture : MonoBehaviour
{
    public Vector2 uvSpeed = new Vector2(-0.1f, -0.1f);

    private MeshRenderer meshRenderer;

    private Vector2 uv;
    
    // Start is called before the first frame update
    void Start()
    {
        uv = new Vector2(0.0f, 0.0f);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        uv += uvSpeed * Time.deltaTime;
        uv.x = uv.x % 1.0f;
        uv.y = uv.y % 1.0f;
        meshRenderer.material.SetFloat("_UDisplacement", uv.x);
        meshRenderer.material.SetFloat("_VDisplacement", uv.y);
    }
}
