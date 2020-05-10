using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSerialVibrate : MonoBehaviour
{
    [SerializeField] Interface.SerialHandler serialHandler;
    [SerializeField] Button[] buttons;
    bool[] onOff = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            onOff[i] = false;
            int j = i;
            buttons[j].onClick.AddListener(() => ButtonON(j));
        }
        serialHandler.Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartSerial()
    {
        var time = Time.deltaTime;
        serialHandler.Open();
        var time1 = Time.deltaTime - time;
        yield break;
    }

    private void ButtonON(int num)
    {
        switch (num)
        {
            case 0:                
                if (onOff[0])
                {
                    serialHandler.Write("h");
                    onOff[0] = false;
                }
                else
                {
                    serialHandler.Write("a");
                    onOff[0] = true;
                }

                Debug.Log(serialHandler.serialPort.IsOpen);
                break;
            case 1:
                if (onOff[1])
                {
                    serialHandler.Write("i");
                    onOff[1] = false;
                }
                else
                {
                    serialHandler.Write("b");
                    onOff[1] = true;
                }

                break;
            case 2:
                if (onOff[2])
                {
                    serialHandler.Write("j");
                    onOff[2] = false;
                }
                else
                {
                    serialHandler.Write("c");
                    onOff[2] = true;
                }

                break;
            case 3:
                if (onOff[0])
                {
                    serialHandler.Write("k");
                    onOff[3] = false;
                }
                else
                {
                    serialHandler.Write("d");
                    onOff[3] = true;
                }

                break;
        }
    }
}
