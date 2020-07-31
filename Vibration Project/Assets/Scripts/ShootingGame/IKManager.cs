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

        public void SetAimHight(float hight)
        {
            var oldPos = aimPoint.localPosition;
            var aimPos = new Vector3(oldPos.x, oldPos.y +hight, oldPos.z);
            aimPoint.localPosition = aimPos;
            //Debug.Log(aimPos);
        }

        private void OnAnimatorIK(int layerIndex)
        {

            if (isIK) ikWeight = 1.0f;
            else ikWeight = 0;
            soldierAnimator.SetLookAtWeight(ikWeight, ikWeight, ikWeight, ikWeight, ikWeight);
            targetPosition = aimPoint.position;
            targetVec = (targetPosition - weaponPoint.position) * 100;
            Debug.DrawLine(weaponPoint.position, targetPosition, Color.blue);
            soldierAnimator.SetLookAtPosition(targetVec);

        }
    }
}

