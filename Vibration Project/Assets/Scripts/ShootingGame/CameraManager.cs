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
        [SerializeField]private float aimAxisY = 0;
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
            aimAxisY -= vertical * Time.deltaTime;
            if(aimAxisY < 0)
            {
                var composer = aimCamera.GetCinemachineComponent<CinemachineComposer>();
                composer.m_ScreenY += vertical * 0.01f;
            }

            else if(aimAxisY > 0)
            {
                aimCamera.m_LookAt.position += new Vector3(0, vertical * Time.deltaTime * 10, 0);
            }
            */

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
            //aimCamera.m_LookAt.position += new Vector3(0, vertical * Time.deltaTime * 10, 0);
            var transposer = aimCamera.GetCinemachineComponent<CinemachineTransposer>();
            var r = Vector3.Distance(aimCamera.transform.position, AimPosition);
            Debug.Log("R = " + r);
            var y = Mathf.Sin(Time.deltaTime * Mathf.PI * 2) * vertical * r;
            var z = Mathf.Cos(Time.deltaTime * Mathf.PI * 2) * vertical * r;
            Debug.Log("y = " + y);
            Debug.Log("z = " + z);
            transposer.m_FollowOffset -= new Vector3(0, y, z);
            
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

