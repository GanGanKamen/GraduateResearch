using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitDirection
{
    Front,
    Back,
    Left,
    Right,
    Error
}

public class VestManager : MonoBehaviour
{
    public float GyiroInput { get { return _gyioInput; } }
    public float Paramater { get { return paramater; } }
    public bool HasSetGyiro { get { return isSetDefultValue; } }
    public HitParts[] DebugHitParts { get { return hitparts; } }
    public const float gyo_y_Max = 250.13f;
    public const float gyo_y_Min = -250.14f;
    [SerializeField] private float oneHitTime;
    [SerializeField] private float errorValue;
    private HitParts[] hitparts;
    public SerialPortUtility.SerialPortUtilityPro SerialPort;
    private float paramater;
    [SerializeField]private float _gyioInput = 0;
    private float defultValue = 0;
    private bool isSetDefultValue = false;
    private float initCount = 0;
    private List<float> initParamater;
    private bool isInit = false;
    // Update is called once per frame
    void Update()
    {
        SetGyiroUpdate();
    }

    public void Init()
    {
        HitPartsInit();
        isInit = true;
        initParamater = new List<float>();
        SerialPort.Close();
        SerialPort.Open();
    }

    public void GyiroInit()
    {
        isSetDefultValue = false;
    }

    public HitDirection AngleToHitDirection(float angle)
    {
        if (angle < 0) return HitDirection.Error;
        var angleDelta = 360f / 8;
        var angleRange = 360f / 4;
        if (angle >= angleDelta && angle < angleDelta + angleRange)
        {
            return HitDirection.Right;
        }
        else if (angle >= angleDelta + angleRange && angle < angleDelta + angleRange * 2)
        {
            return HitDirection.Back;
        }
        else if (angle >= angleDelta + angleRange * 2 && angle < angleDelta + angleRange * 3)
        {
            return HitDirection.Left;
        }
        else return HitDirection.Front;
    }

    public void StartBlood(HitDirection hitDirection)
    {
        switch (hitDirection)
        {
            case HitDirection.Front:
                hitparts[0].Heat(SerialPort, true);
                break;
            case HitDirection.Back:
                hitparts[1].Heat(SerialPort, true);
                break;
            case HitDirection.Left:
                hitparts[2].Heat(SerialPort, true);
                break;
            case HitDirection.Right:
                hitparts[3].Heat(SerialPort, true);
                break;
            case HitDirection.Error:
                break;
        }
    }

    public void StopBlood(HitDirection hitDirection)
    {
        switch (hitDirection)
        {
            case HitDirection.Front:
                hitparts[0].Heat(SerialPort, false);
                break;
            case HitDirection.Back:
                hitparts[1].Heat(SerialPort, false);
                break;
            case HitDirection.Left:
                hitparts[2].Heat(SerialPort, false);
                break;
            case HitDirection.Right:
                hitparts[3].Heat(SerialPort, false);
                break;
            case HitDirection.Error:
                break;
        }
    }

    public void StartHit(HitDirection hitDirection)
    {
        switch (hitDirection)
        {
            case HitDirection.Front:
                hitparts[0].Vibrate(SerialPort, true);
                break;
            case HitDirection.Back:
                hitparts[1].Vibrate(SerialPort, true);
                break;
            case HitDirection.Left:
                hitparts[2].Vibrate(SerialPort, true);
                break;
            case HitDirection.Right:
                hitparts[3].Vibrate(SerialPort, true);
                break;
            case HitDirection.Error:
                break;
        }
    }

    public void StopHit(HitDirection hitDirection)
    {
        switch (hitDirection)
        {
            case HitDirection.Front:
                hitparts[0].Vibrate(SerialPort, false);
                break;
            case HitDirection.Back:
                hitparts[1].Vibrate(SerialPort, false);
                break;
            case HitDirection.Left:
                hitparts[2].Vibrate(SerialPort, false);
                break;
            case HitDirection.Right:
                hitparts[3].Vibrate(SerialPort, false);
                break;
            case HitDirection.Error:
                break;
        }
    }

    public void OneHit(HitDirection HitDirection)
    {
        var targetNum = 0;
        for (int i = 0; i < hitparts.Length; i++)
        {
            if (hitparts[i].HitDirection == HitDirection)
            {
                targetNum = i;
                break;
            }
        }
        if (hitparts[targetNum].IsVibrate) return;
        StartCoroutine(VibrateCourtine(HitDirection));
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
        if (isSetDefultValue == false)
        {
            initParamater.Add(val);
        }

        else
        {
            if (paramater > defultValue + errorValue ||
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
                        result = Map(paramater, gyo_y_Min, defultValue - errorValue, -1, 0);
                        break;
                }
                _gyioInput = result;
            }
            else
            {
                _gyioInput = 0;
            }
        }
    }

    public void StopAllBlood()
    {
        for(int i = 0; i < hitparts.Length; i++)
        {
            if(hitparts[i].IsHeat)
            hitparts[i].Heat(SerialPort, false);
        }
    }

    public void StopAllHit()
    {
        StopAllCoroutines();
        for (int i = 0; i < hitparts.Length; i++)
        {
            if(hitparts[i].IsVibrate)
            hitparts[i].Vibrate(SerialPort, false);
        }
    }

    private void HitPartsInit()
    {
        hitparts = new HitParts[4];
        for(int i = 0; i < 4; i++)
        {
            hitparts[0] = new HitParts(HitDirection.Front, "o", "p", "m", "n");
            hitparts[1] = new HitParts(HitDirection.Back, "c", "d", "a", "b");
            hitparts[2] = new HitParts(HitDirection.Left, "g", "h", "e", "f");
            hitparts[3] = new HitParts(HitDirection.Right, "k", "l", "i", "j");
        }
    }

    private void SetGyiroUpdate()
    {
        if (isInit == false) return;
        if (isSetDefultValue == false)
        {
            initCount += Time.deltaTime;
            if (initCount >= 5)
            {
                initCount = 0;
                float defultValueCount = 0;
                for (int i = 0; i < initParamater.Count; i++)
                {
                    defultValueCount += initParamater[i];
                }
                defultValue = defultValueCount / initParamater.Count;
                Debug.Log(defultValue);
                isSetDefultValue = true;
            }
        }
    }

    private IEnumerator VibrateCourtine(HitDirection HitDirection)
    {
        var targetNum = 0;
        for(int i = 0; i < hitparts.Length; i++)
        {
            if(hitparts[i].HitDirection == HitDirection)
            {
                targetNum = i;
                break;
            }
        }
        if (hitparts[targetNum].IsVibrate) yield break;
        hitparts[targetNum].Vibrate(SerialPort, true);
        yield return new WaitForSeconds(oneHitTime);
        hitparts[targetNum].Vibrate(SerialPort, false);
        yield break;
    }

    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}
