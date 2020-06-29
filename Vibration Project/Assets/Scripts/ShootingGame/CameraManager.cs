using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Shooting
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineBrain CharacterCamera { get { return characterCamera; } }
        [SerializeField] private CinemachineBrain characterCamera;
        [SerializeField] private CinemachineFreeLook freeLookCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private Transform aimCenter;
        [Range(-0.999f, -0.5f)]
        private float minYAngle = -0.5f;
        [Range(0.5f, 0.999f)]
        private float maxYAngle = 0.5f;

        private Quaternion defualtAimRot;
        // Start is called before the first frame update
        void Start()
        {
            aimCamera.transform.parent = aimCamera.m_LookAt;
            defualtAimRot = aimCamera.m_LookAt.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(aimCamera.transform.position,aimCamera.m_LookAt.position,Color.red);
            aimCamera.transform.LookAt(aimCenter.position);
        }
        public void TrunAimCamera()
        {
            aimCamera.Priority = 11;
        }

        public void CancelAimCamera()
        {
            aimCamera.Priority = 0;
        }

        public void FreeCameraReset()
        {
            freeLookCamera.m_XAxis.Value = 1;
            freeLookCamera.m_YAxis.Value = 0.5f;
        }

        public void AimCameraReset()
        {
            aimCamera.m_LookAt.localRotation = defualtAimRot;
        }
    
        public void AimCameraRotate(float vertical)
        {
            var nowAngle = aimCamera.m_LookAt.localRotation.x;
            if (0 < vertical)
            {
                if (minYAngle <= nowAngle)
                {
                    aimCamera.m_LookAt.transform.Rotate(-vertical, 0, 0);
                }
            }
            else if(0 > vertical)
            {
                if (nowAngle <= maxYAngle)
                {
                    aimCamera.m_LookAt.transform.Rotate(-vertical, 0, 0);
                }
            }
        }

        public Vector3 FreeCameraVec(Vector3 pos)
        {
            return (pos - freeLookCamera.transform.position).normalized;
        }

    }
}

