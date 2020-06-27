using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class PlayerController : SoldierBase
    {
        [SerializeField] private CameraManager cameraManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            KeyCtrl();
        }

        private void KeyCtrl()
        {
            var ax = Input.GetAxis("Horizontal");
            var az = Input.GetAxis("Vertical");
            var inputVec = new Vector3(ax, 0, az);
            //Debug.Log(new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y")));           

            switch (IsAim)
            {
                case false:
                    if (inputVec.magnitude != 0)
                    {
                        var cameraFoward = cameraManager.CharacterCamera.transform.forward.normalized;
                        cameraFoward = Vector3.Scale(cameraFoward, new Vector3(1, 0, 1));
                        var cameraRight = cameraManager.CharacterCamera.transform.right.normalized;
                        cameraRight = Vector3.Scale(cameraRight, new Vector3(1, 0, 1));
                        var moveDirection = cameraFoward * az + cameraRight * ax;
                        CharacterMove(moveDirection);
                    }
                    else CharacterStand();
                    break;
                case true:
                    //var hight = (transform.position - cameraManager.CharacterCamera.transform.position).y + 2.5f;
                    AimRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    cameraManager.AimCameraMove(Input.GetAxis("Mouse Y"));
                    if (inputVec.magnitude != 0)
                    {
                        var inputVec2 = new Vector2(inputVec.x, inputVec.z);
                        AimMove(inputVec2);
                    }
                    else CharacterStand();
                    break;
            }

            

            if (Input.GetMouseButton(1))
            {
                if (IsAim == false)
                {
                    var vec = cameraManager.FreeCameraVec(transform.position);
                    SetTransformRotation(vec);
                    SetAiming(true);
                    cameraManager.TrunAimCamera();
                }
            }
            else
            {
                if (IsAim)
                {
                    SetAiming(false);
                    cameraManager.CancelAimCamera();
                    cameraManager.FreeCameraReset();
                    cameraManager.AimCameraReset();
                }
            }
        }
    }
}

