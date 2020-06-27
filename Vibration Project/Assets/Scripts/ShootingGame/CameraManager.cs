using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Shooting
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineBrain CharacterCamera { get { return characterCamera; } }
        public Vector3 AimCameraVec { get { return (aimCamera.m_LookAt.position - aimCamera.transform.position).normalized; } }

        public Vector3 AimPosition { get { return aimCamera.m_LookAt.position; } }
        [SerializeField] private CinemachineBrain characterCamera;
        [SerializeField] private CinemachineFreeLook freeLookCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private Vector2 aimLimit;

        private Vector3 defaultAimOffest;
        // Start is called before the first frame update
        void Start()
        {
            var transposer = aimCamera.GetCinemachineComponent<CinemachineTransposer>();
            defaultAimOffest = transposer.m_FollowOffset;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(aimCamera.transform.position,aimCamera.m_LookAt.position,Color.red);
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
            var transposer = aimCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = defaultAimOffest;
            var composer = aimCamera.GetCinemachineComponent<CinemachineComposer>();
            composer.m_ScreenY = 0.5f;
        }
    
        public void AimCameraMove(float vertical)
        {
            /*
            var composer = aimCamera.GetCinemachineComponent<CinemachineComposer>();
            composer.m_ScreenY += vertical * 0.01f;
            if (composer.m_ScreenY < aimLimit.x)
            {
                composer.m_ScreenY = aimLimit.x;
                return;
            } 
            else if (composer.m_ScreenY > aimLimit.y)
            {
                composer.m_ScreenY = aimLimit.y;
                return;
            }
            var transposer = aimCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset -= new Vector3(0, vertical * 0.1f, 0);
            */
            aimCamera.m_LookAt.position += new Vector3(0, vertical * Time.deltaTime * 10,0);
        }

        public void AimCameraRotate(float vertical)
        {
            aimCamera.transform.RotateAround(aimCamera.m_LookAt.position,
                Vector3.right, vertical * Time.deltaTime);
        }

        public Vector3 FreeCameraVec(Vector3 pos)
        {
            return (pos - freeLookCamera.transform.position).normalized;
        }

    }
}

