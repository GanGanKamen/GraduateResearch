using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyiroAngle : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private UnityEngine.UI.Text paramaterText;
    [SerializeField] private UnityEngine.UI.Text InputText;
    [SerializeField] private float errorValue;
    public const float gyo_y_Max = 250.13f;
    public const float gyo_y_Min = -250.14f;
    private float paramater;
    private float input = 0;
    private bool isInit = false;
    private float defultValue = 0;
    private float initCount = 0;

    private List<float> initParamater;
    // Start is called before the first frame update
    void Start()
    {
        initParamater = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit == false)
        {
            initCount += Time.deltaTime;
            if(initCount >= 5)
            {
                initCount = 0;
                float defultValueCount = 0;
                for (int i = 0; i < initParamater.Count; i++)
                {
                    defultValueCount += initParamater[i];
                }
                defultValue = defultValueCount / initParamater.Count;
                Debug.Log(defultValue);
                isInit = true;
            }
        }

        else
        {
            target.eulerAngles += new Vector3(0, speed * Time.deltaTime* -input, 0);
        }
        paramaterText.text = "Gyio:  " + paramater.ToString();
        InputText.text = "Input:  " + input.ToString();
    }

    public void ReadComplateString(object data)
    {

        var text = data as string;
        string[] arr = text.Split(',');

        if (arr.Length < 1) return;
        var canPerse = float.TryParse(arr[0], out paramater);
        if (canPerse) paramater = float.Parse(arr[0]);
        if (paramater > gyo_y_Max) paramater = gyo_y_Max;
        if (paramater < gyo_y_Min) paramater = gyo_y_Min;
        var val = paramater;

        if (isInit == false)
        {
            initParamater.Add(val);
        }

        else
        {
            if(paramater > defultValue + errorValue || 
                paramater < defultValue - errorValue)
            {
                float result = 0;
                bool dir = paramater > defultValue;
                switch (dir)
                {
                    case true:
                        result = Map(paramater, defultValue + errorValue, gyo_y_Max, 0, 1);
                        break;
                    case false:
                        result = Map(paramater, gyo_y_Min, defultValue - errorValue,  -1, 0);
                        break;
                }

                input = result;
            }
            else
            {
                input = 0;
            }
        }
    }

    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}
