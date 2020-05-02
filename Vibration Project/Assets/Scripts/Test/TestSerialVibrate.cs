using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSerialVibrate : MonoBehaviour
{
    [SerializeField] Interface.SerialHandler serialHandler;
    [SerializeField] Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[j].onClick.AddListener(() => ButtonON(j));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonON(int num)
    {
        switch (num)
        {
            case 0:
                serialHandler.Write("a");
                break;
            case 1:
                serialHandler.Write("b");
                break;
            case 2:
                serialHandler.Write("c");
                break;
            case 3:
                serialHandler.Write("d");
                break;
        }
        Debug.Log(num);
    }
}
