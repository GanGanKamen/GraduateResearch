using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationEffect:ScriptableObject
    {
        public AnimationCurve LeftMoterCurve;
        public AnimationCurve RightMoterCurve;
        
        public VibrationEffect(AnimationCurve _left,AnimationCurve _right)
        {
            LeftMoterCurve = _left;
            RightMoterCurve = _right;
        }
    }
}


