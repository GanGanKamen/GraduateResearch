using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVector : MonoBehaviour
{
    [SerializeField] Transform lookat;
    [SerializeField] Transform cam1;
    [SerializeField] Transform cam0;

    [SerializeField] float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vec0 = (cam0.position - lookat.position).normalized;
        var vec1 = (cam1.position - lookat.position).normalized;
        angle = Vector3.Angle(vec0, vec1);
    }
}
