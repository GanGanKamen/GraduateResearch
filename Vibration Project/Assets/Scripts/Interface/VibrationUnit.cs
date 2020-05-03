using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public enum VibrationUnitType
    {
        Proto,
        DX
    }

    public class VibrationUnit
    {
        public UnitCategoly VibrationUnitCategoly;
        public bool IsVibrating { get { return isVibrating; } }

        private bool isVibrating = false;
        private VibrationUnitType type = VibrationUnitType.Proto;

        public VibrationUnit(UnitCategoly _unitCategoly, VibrationUnitType _type)
        {
            VibrationUnitCategoly = _unitCategoly;
            type = _type;
        }

        public void PlayVibrationOneShot(VibrationSystem vibrationSystem, float leftPower, float rightPower,float time)
        {
            vibrationSystem.StartCoroutine(OneShotCoroutine(leftPower, rightPower, time));
        }
        
        private IEnumerator OneShotCoroutine(float leftPower, float rightPower, float time)
        {
            if (isVibrating) yield break;
            isVibrating = true;
            var direction = UnitCategolyToPlayerIndex();
            XInputDotNetPure.GamePad.SetVibration(direction, leftPower, rightPower);
            yield return new WaitForSeconds(time);
            XInputDotNetPure.GamePad.SetVibration(direction, 0, 0);
            isVibrating = false;
            yield break;
        }
        
        private IEnumerator VibrationEffectCoroutine(VibrationEffect vibrationEffect)
        {
            if (isVibrating) yield break;
            isVibrating = true;
            GameObject ctrlObj = new GameObject("VibrationControll");
            ctrlObj.AddComponent<VibrationControll>();
            ctrlObj.GetComponent<VibrationControll>().Init(vibrationEffect, UnitCategolyToPlayerIndex());
            var left = vibrationEffect.LeftMoterCurve;
            var right = vibrationEffect.RightMoterCurve;
            float time = Mathf.Max(left.keys[left.length - 1].time,right.keys[right.length -1].time);
            yield return new WaitForSeconds(time);
            GameObject.Destroy(ctrlObj);
            StopVibration();
            yield break;
        }

        public void PlayVibration(float leftPower,float rightPower)
        {
            if (isVibrating || type != VibrationUnitType.Proto) return;
            var direction = UnitCategolyToPlayerIndex();
            XInputDotNetPure.GamePad.SetVibration(direction, leftPower, rightPower);
            isVibrating = true;
        }

        public void StopVibration()
        {
            if (isVibrating == false) return;
            switch (type)
            {
                case VibrationUnitType.Proto:
                    var direction = UnitCategolyToPlayerIndex();
                    XInputDotNetPure.GamePad.SetVibration(direction, 0, 0);
                    break;
            }
            isVibrating = false;
        }

        private XInputDotNetPure.PlayerIndex UnitCategolyToPlayerIndex()
        {
            switch (VibrationUnitCategoly)
            {
                case UnitCategoly.Foward:
                    return XInputDotNetPure.PlayerIndex.One;
                case UnitCategoly.Left:
                    return XInputDotNetPure.PlayerIndex.Two;
                case UnitCategoly.Back:
                    return XInputDotNetPure.PlayerIndex.Three;
                case UnitCategoly.Right:
                    return XInputDotNetPure.PlayerIndex.Four;
                default:
                    return 0;
            }
        }

        /*
        public void PlayVibration(bool i_enabled)

        {

            float motorPower = i_enabled ? 1.0f : 0.0f;

            XInputDotNetPure.GamePad.SetVibration(0, motorPower, motorPower);

        }*/
    }
}


