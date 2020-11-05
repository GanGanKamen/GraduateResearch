using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyiroAngle : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private UnityEngine.UI.Text text1;
    private float[] paramaters;
    // Start is called before the first frame update
    void Start()
    {
        paramaters = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, paramaters[1], 0);
        text1.text = paramaters[0] + "," + paramaters[1] + "," + paramaters[2] + ",";
    }

    public void ReadComplateString(object data)
    {

        var text = data as string;
        string[] arr = text.Split(',');

        if (arr.Length < 3) return;
        for (int i = 0; i < 3; i++)
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
