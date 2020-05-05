using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSerial : MonoBehaviour
{
    [SerializeField]Interface.SerialHandler serialHandler;
    [SerializeField] Button onButton;
    [SerializeField] Button offButton;

    // Start is called before the first frame update
    void Start()
    {
        onButton.onClick.AddListener(() => LED_ON());
        offButton.onClick.AddListener(() => LED_OFF());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LED_ON()
    {
        serialHandler.Write("a1;");
        Debug.Log("WriteA");
    }

    private void LED_OFF()
    {
        serialHandler.Write("a0;");
        Debug.Log("Write0");
    }
}
