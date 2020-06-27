using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class IKManager : MonoBehaviour
    {
        public bool IsIK { get { return isIK; } }
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private float ikWeight = 0;
        [SerializeField] private Animator soldierAnimator;
        private Vector3 targetVec;
        private Vector3 targetPosition;
        private Vector3 startPos;
        private Vector3 endPos;
        private bool isIK;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetIK()
        {
            isIK = true;

        }

        public void ResetIK()
        {
            isIK = false;
        }
        
        public void SetTargetVec(Vector3 _startPos,Vector3 _endPos)
        {
            targetPosition = endPos;
            startPos = _startPos;
            endPos = _endPos;

        }
        

        public void SetTarget(Vector3 pos)
        {
            targetPosition = pos;
        }

        private void OnAnimatorIK(int layerIndex)
        {

            if (isIK) ikWeight = 1.0f;
            else ikWeight = 0;
            soldierAnimator.SetLookAtWeight(ikWeight, ikWeight, ikWeight, ikWeight, ikWeight);
            targetVec = (endPos - startPos) * 100;
            Debug.DrawLine(startPos, endPos, Color.blue);
            soldierAnimator.SetLookAtPosition(targetVec);
            /*
            soldierAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
            soldierAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);

            soldierAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            soldierAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);

            //soldierAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
            soldierAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
            */
        }
    }
}

