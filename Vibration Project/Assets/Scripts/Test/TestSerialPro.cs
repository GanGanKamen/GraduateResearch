using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SerialPortUtility;

public class TestSerialPro : MonoBehaviour
{
    SerialPortUtilityPro serialHandler;
    [SerializeField] Button[] buttons;
    [SerializeField] Button button_Start;
    [SerializeField] string nextSceneName;
    bool[] onOff = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        serialHandler = GetComponent<SerialPortUtilityPro>();
        for (int i = 0; i < buttons.Length; i++)
        {
            onOff[i] = false;
            int j = i;
            buttons[j].onClick.AddListener(() => ButtonON(j));
        }
        button_Start.onClick.AddListener(() => NextScene());
    }

    private IEnumerator Setup()
    {
        serialHandler = GetComponent<SerialPortUtilityPro>();
        for (int i = 0; i < buttons.Length; i++)
        {
            onOff[i] = false;
            int j = i;
            buttons[j].onClick.AddListener(() => ButtonON(j));
        }
        var time0 = Time.deltaTime;
        while(serialHandler.IsOpened() == false)
        {
            yield return null;
        }
        var time1 = Time.deltaTime - time0;
        Debug.Log("SetUpOver " + time1);
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

    private void ButtonON(int num)
    {
        switch (num)
        {
            case 0:
                if (onOff[0])
                {
                    XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
                    onOff[0] = false;
                }
                else
                {
                    XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
                    onOff[0] = true;
                }

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
                if (onOff[3])
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

    /*
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
                if (onOff[3])
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
        
        switch (onOff[num])
        {
            case true:
                buttons[num].image.color = Color.red;
                break;
            case false:
                buttons[num].image.color = Color.white;
                break;
        }
    }
    */
}
