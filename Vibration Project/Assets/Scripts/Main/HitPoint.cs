using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint
{
    public bool IsBlood { get { return isBlood; } }
    private bool isBlood = false;
    private int _endurance = 0;
    //private Vector2 _angleRange = new Vector2(0,0);   // x <= angle < y
    private float _recoveryTime = 0;
    private float nowRecoveryTime = 0;

    public HitPoint(int endurance,float recoveryTime)
    {
        if(endurance > 0)_endurance = endurance;
        //if(angleRange.x < angleRange.y)_angleRange = angleRange;
        if(recoveryTime >0)_recoveryTime = recoveryTime;
    }

    public void GetDamege()
    {
        if (_endurance <= 0 || isBlood) return;
        _endurance -= 1;
        if (_endurance == 0) isBlood = true; 
    } 

    public void Recovery() //update
    {
        if (_recoveryTime < 0) return;
        if (isBlood)
        {
            if (nowRecoveryTime < _recoveryTime)
                nowRecoveryTime += Time.deltaTime;
            else
            {
                nowRecoveryTime = 0;
                isBlood = false;
            }
        }
    }
}
