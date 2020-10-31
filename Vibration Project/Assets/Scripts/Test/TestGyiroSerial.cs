using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyiroSerial : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private Vector2 range;
    [SerializeField] private float rotX; private float preX;
    [SerializeField] private float rotY; private float preY;
    [SerializeField] private float rotZ; private float preZ;

    [SerializeField] private float inputX;
    [SerializeField] private float inputY;
    [SerializeField] private float inputZ;

    private bool isInit = false;
    private float count = 0;

    private float defRotX = -6f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInit == false)
        {
            count += Time.deltaTime;
            if (count >= 2)
            {
                count = 0;
                isInit = true;
            }
        }
    }

    public void ReadComplateString(object data)
    {
        var text = data as string;
        string[] arr = text.Split(',');

        if (arr.Length < 3) return;
        bool canRoll = float.TryParse(arr[0], out rotX);
        if (canRoll) rotX = float.Parse(arr[0]);
        bool canPitch = float.TryParse(arr[1], out rotY);
        if (canPitch) rotY = float.Parse(arr[1]);
        bool canYaw = float.TryParse(arr[2], out rotZ);
        if (canYaw) rotZ = float.Parse(arr[2]);

        switch (isInit)
        {
            case false:
                preX = rotX;
                preY = rotY;
                preZ = rotZ;
                break;
            case true:
                var rotDeltaX = Mathf.Abs(rotX - defRotX);
                if (rotDeltaX > range.x)
                {
                    if (rotDeltaX > range.y) rotDeltaX = range.y;
                    bool trigger = rotX > defRotX;
                    switch (trigger)
                    {
                        case true:
                            var val = rotX - defRotX;
                            inputX = Map(val,range.x + defRotX,range.y + defRotX,0,1);
                            if (inputX < 0.1f) inputX = 0;
                            break;
                        case false:
                            //var val1 = defRotX- rotX;
                            //inputX = Map(val1, range.x - defRotX, range.y - defRotX, -1, 0);
                            //if (inputX > -0.1f) inputX = 0;
                            break;
                    }
                }
                /*
                var rotDeltaX = Mathf.Abs(rotX - preX);
                if (rotDeltaX > range.x)
                {
                    if (rotX > range.y) rotDeltaX = range.y;
                    bool trigger = rotDeltaX > preX;
                    switch (trigger)
                    {
                        case true:
                            inputX = Map(rotDeltaX - preX, range.x - preX, range.y + preX, 0, 1);
                            if (inputX < 0.1f) inputX = 0;
                            break;
                        case false:
                            inputX = Map(preX - rotDeltaX, preX - range.y, preX - range.x, -1, 0);
                            if (inputX > -0.1f) inputX = 0;
                            break;
                    }
                }
                else preX = rotX;
                
                var rotDeltaY = Mathf.Abs(rotY - preY);
                if (rotDeltaY > range.x)
                {
                    if (rotDeltaY > range.y) rotDeltaY = range.y;
                    bool trigger = rotDeltaY > preY;
                    switch (trigger)
                    {
                        case true:
                            inputY = Map(rotDeltaX - preY, range.x - preY, range.y + preY, 0, 1);
                            break;
                        case false:
                            inputY = Map(preY - rotDeltaX, preY - range.y, preY - range.x, -1, 0);
                            break;
                    }
                }
                else preY = rotY;
                */


                break;
        }
    }

    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}


