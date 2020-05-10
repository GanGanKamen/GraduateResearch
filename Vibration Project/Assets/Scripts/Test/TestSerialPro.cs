using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerialPortUtility;

public class TestSerialPro : MonoBehaviour
{
    SerialPortUtilityPro serial;
    // Start is called before the first frame update
    void Start()
    {
        serial = GetComponent<SerialPortUtilityPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            serial.Write("a1;");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            serial.Write("a0;");
        }
    }
}
