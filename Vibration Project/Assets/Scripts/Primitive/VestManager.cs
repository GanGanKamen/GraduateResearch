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
    public bool IsFrontBlood { get { return _isFrontBlood; } }
    public bool IsLeftBlood { get { return _isLeftBlood; } }
    public bool IsRightBlood { get { return _isRightBlood; } }
    public bool IsBackBlood { get { return _isBackBlood; } }

    private bool _isFrontBlood = false;
    private bool _isLeftBlood = false;
    private bool _isRightBlood = false;
    private bool _isBackBlood = false;

    private SerialPortUtility.SerialPortUtilityPro SerialPort;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        SerialPort = GetComponent<SerialPortUtility.SerialPortUtilityPro>();
        SerialPort.Open();
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

    public void FrontBlood(bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (_isFrontBlood) return;
                SerialPort.Write("a");
                _isFrontBlood = true;
                break;
            case false:
                if (_isFrontBlood == false) return;
                SerialPort.Write("a");
                _isFrontBlood = false;
                break;
        }
    }

    public void LeftBlood(bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (_isLeftBlood) return;
                SerialPort.Write("a");
                _isLeftBlood = true;
                break;
            case false:
                if (_isLeftBlood == false) return;
                SerialPort.Write("a");
                _isLeftBlood = false;
                break;
        }
    }

    public void RightBlood(bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (_isRightBlood) return;
                SerialPort.Write("a");
                _isRightBlood = true;
                break;
            case false:
                if (_isRightBlood == false) return;
                SerialPort.Write("a");
                _isRightBlood = false;
                break;
        }
    }

    public void BackBlood(bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (_isBackBlood) return;
                SerialPort.Write("a");
                _isBackBlood = true;
                break;
            case false:
                if (_isBackBlood == false) return;
                SerialPort.Write("a");
                _isBackBlood = false;
                break;
        }
    }

    private IEnumerator VibrateCourtine(HitDirection HitDirection)
    {
        string startMessage = "";
        string stopMessage = "";
        switch (HitDirection)
        {
            case HitDirection.Front:

                break;
            case HitDirection.Back:
                break;
            case HitDirection.Left:
                break;
            case HitDirection.Right:
                break;
        }
        yield break;
    }
}
