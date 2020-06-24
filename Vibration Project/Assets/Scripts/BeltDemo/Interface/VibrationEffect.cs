using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationEffect:MonoBehaviour
    {
        public AnimationCurve LeftMoterCurve;
        public AnimationCurve RightMoterCurve;
        
        public VibrationEffect(AnimationCurve _left,AnimationCurve _right)
        {
            LeftMoterCurve = _left;
            RightMoterCurve = _right;
        }

        public void Init(AnimationCurve _left, AnimationCurve _right)
        {
            LeftMoterCurve = _left;
            RightMoterCurve = _right;
        }
    }
}


