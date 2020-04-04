using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationUnit
    {
        public UnitCategoly VibrationUnitCategoly;

        public VibrationUnit(UnitCategoly _unitCategoly)
        {
            VibrationUnitCategoly = _unitCategoly;
        }

        public void PlayVibrationOneShot(float leftPower, float rightPower,float time)
        {
            var direction = UnitCategolyToPlayerIndex();
            XInputDotNetPure.GamePad.SetVibration(direction, leftPower, rightPower);
            float progressTime = 0;
            do
            {
                progressTime += Time.deltaTime;
                Debug.Log(progressTime);
            } while (progressTime <= time);
            XInputDotNetPure.GamePad.SetVibration(direction, 0, 0);
        }

        public void PlayVibration(float leftPower,float rightPower)
        {
            var direction = UnitCategolyToPlayerIndex();
            XInputDotNetPure.GamePad.SetVibration(direction, leftPower, rightPower);
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


