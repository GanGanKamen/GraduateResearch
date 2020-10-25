using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class IKManager : MonoBehaviour
    {
        public bool IsIK { get { return isIK; } }
        [SerializeField] private float ikWeight = 0;
        [SerializeField] private Animator soldierAnimator;
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private float aimErrorHight;
        [SerializeField] private float aimErrorHorizon;
        private Vector3 targetVec;
        private Vector3 targetPosition;
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

        public void SetAimHight(Transform target)
        {
            var targetPos = target.position;
            var aimPos = targetPos;
            aimPoint.position = aimPos;
        }

        private void OnAnimatorIK(int layerIndex)
        {

            if (isIK) ikWeight = 1.0f;
            else ikWeight = 0;
            soldierAnimator.SetLookAtWeight(ikWeight, ikWeight, ikWeight, ikWeight, ikWeight);
            targetPosition = aimPoint.position;
            var lookAtPosition = transform.right * aimErrorHorizon + aimPoint.position
                - new Vector3(0, aimErrorHight,0);
            soldierAnimator.SetLookAtPosition(lookAtPosition);

        }
    }
}

