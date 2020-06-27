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

        public void SetTargetVec(Vector3 vec)
        {
            targetVec = vec;
        }

        private void OnAnimatorIK(int layerIndex)
        {

            if (isIK) ikWeight = 1.0f;
            else ikWeight = 0;
            soldierAnimator.SetLookAtWeight(ikWeight, ikWeight, ikWeight, ikWeight, ikWeight);
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

