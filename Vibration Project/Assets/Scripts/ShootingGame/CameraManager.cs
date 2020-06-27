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
        [SerializeField] private Vector2 aimLimit;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
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
            transposer.m_FollowOffset = new Vector3(0.6f,2.5f , -1f);
            var composer = aimCamera.GetCinemachineComponent<CinemachineComposer>();
            composer.m_ScreenY = 0.5f;
        }
    
        public void AimCameraMove(float vertical)
        {
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

        }

        public Vector3 FreeCameraVec(Vector3 pos)
        {
            return (pos - freeLookCamera.transform.position).normalized;
        }

        public Vector3 AimCameraVec(Vector3 pos)
        {
            return (pos - aimCamera.transform.position).normalized;
        }
    }
}

