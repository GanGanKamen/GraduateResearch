using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBeam : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] Transform hitPoint;

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, hitPoint.position);
    }
}
