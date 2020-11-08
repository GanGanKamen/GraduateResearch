using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerialPortUtility;

public class HitParts
{
    public bool IsHeat { get { return isHeat; } }
    public bool IsVibrate { get { return isVibrate; } }

    private HitDirection _hitDirection = HitDirection.Error;
    private string _startHeat = "";
    private string _stopHeat = "";
    private string _startVibrate = "";
    private string _stopVibrate = "";

    private bool isHeat = false;
    private bool isVibrate = false;

    public HitParts(HitDirection hitDirection,
        string startHeat,string stopHeat,
        string startVibrate,string stopVibrate)
    {
        _hitDirection = hitDirection;
        _startHeat = startHeat;
        _stopHeat = stopHeat;
        _startVibrate = startVibrate;
        _stopVibrate = stopVibrate;
    }

    public void Heat(SerialPortUtilityPro serialPort, bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (isHeat) return;
                serialPort.Write(_startHeat);
                isHeat = true;
                break;
            case false:
                if (isHeat == false) return;
                serialPort.Write(_stopHeat);
                isHeat = false;
                break;
        }
    }

    public void Vibrate(SerialPortUtilityPro serialPort, bool StartOrStop)
    {
        switch (StartOrStop)
        {
            case true:
                if (isVibrate) return;
                serialPort.Write(_startVibrate);
                isVibrate = true;
                break;
            case false:
                if (isVibrate == false) return;
                serialPort.Write(_stopVibrate);
                isVibrate = false;
                break;
        }
    }
}
