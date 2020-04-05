using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorMath : MonoBehaviour
{
    [SerializeField] float num;
    // Start is called before the first frame update
    void Start()
    {
        var result = Mathf.Floor(num);
        Debug.Log(result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
