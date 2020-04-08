using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PluginTest : MonoBehaviour
{
    [DllImport("SAMPLEDLL3")] private static extern int CountUp();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CountUp());
    }
}
