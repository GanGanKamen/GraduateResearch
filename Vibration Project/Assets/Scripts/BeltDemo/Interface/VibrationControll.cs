using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationControll : MonoBehaviour
    {
        public VibrationEffect effect;
        public XInputDotNetPure.PlayerIndex index;
        private float processTime = 0;
        private float leftPower;
        private float rightPower;

        public VibrationControll(VibrationEffect _effect, XInputDotNetPure.PlayerIndex _index)
        {
            effect = _effect;
            index = _index;
        }

        public void Init(VibrationEffect _effect, XInputDotNetPure.PlayerIndex _index)
        {
            effect = _effect;
            index = _index;
        }

        void Update()
        {
            processTime += Time.deltaTime;
            leftPower = effect.LeftMoterCurve.Evaluate(processTime);
            rightPower = effect.RightMoterCurve.Evaluate(processTime);
            XInputDotNetPure.GamePad.SetVibration(index, leftPower, rightPower);
        }
    }
}


