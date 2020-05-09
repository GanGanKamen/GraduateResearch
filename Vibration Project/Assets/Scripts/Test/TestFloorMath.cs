using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorMath : MonoBehaviour
{
    [SerializeField] float angle;
    [SerializeField] int num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        num = AngleToDirectionNumber(angle);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (num >= 7)
            {
                Debug.Log("0, 3");
            }
            else if (num % 2 == 0)
            {
                var unitNum = num / 2;
                Debug.Log(unitNum);

            }
            else
            {
                var unitNum0 = (num + 1) / 2;
                var unitNum1 = (num - 1) / 2;
                Debug.Log(unitNum0.ToString() + "," + unitNum1.ToString());
            }
        }
        
    }

    private int AngleToDirectionNumber(float angle)
    {
        float angleDelta = 360f / 8f;
        float startAngle = 0 + angleDelta / 2f;
        if (angle > startAngle && angle <= 360f - startAngle)
        {
            return (int)Mathf.Ceil((angle - startAngle) / angleDelta);
        }
        else
        {
            return 0;
        }
    }
}
