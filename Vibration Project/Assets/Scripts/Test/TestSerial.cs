using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSerial : MonoBehaviour
{
    [SerializeField]Interface.SerialHandler serialHandler;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            serialHandler.Write("a");
            Debug.Log("WriteA");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            serialHandler.Write("s");
            Debug.Log("WriteS");
        }
    }
}
