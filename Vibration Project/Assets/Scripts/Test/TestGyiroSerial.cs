using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyiroSerial : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private float speed;
    [SerializeField] private float input;
    private float[] paramaters;
    private float[] preParas;

    private float count = 0;
    private bool isInit = false;
    void Start()
    {
        paramaters = new float[6];
        preParas = new float[6];
    }

    // Update is called once per frame
    void Update()
    {
        input = paramaters[4];
        if (isInit == false)
        {
            count += Time.deltaTime;
            if (count >= 2)
            {
                count = 0;
                isInit = true;
            }
        }
        else
        {
            if (paramaters[4] <= -10) transform.eulerAngles += new Vector3(0, Time.deltaTime * speed, 0);
            if(paramaters[4] >= 10) transform.eulerAngles -= new Vector3(0, Time.deltaTime * speed, 0);
            /*
            var diffrence = Mathf.Abs(paramaters[4] - preParas[4]);
            if (paramaters[4] >= 20)
            {
                if (paramaters[4] > preParas[4]) Debug.Log("1");
                else Debug.Log("-1");
            }
            preParas[4] = paramaters[4];
            */
        }
    }

    public void ReadComplateString(object data)
    {
        
        var text = data as string;
        string[] arr = text.Split(',');

        if (arr.Length < 6) return;
        for(int i = 0; i< 6; i++)
        {
            var canPerse = float.TryParse(arr[i], out paramaters[i]);
            if (canPerse) paramaters[i] = float.Parse(arr[i]);          
        } 
        
    }

    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}


