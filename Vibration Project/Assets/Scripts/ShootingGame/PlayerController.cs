using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class PlayerController : SoldierBase
    {
        [SerializeField] private Transform characterCamera;
        [SerializeField] private Transform right;
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
            var cameraFoward = (Body.transform.position - characterCamera.position).normalized;
            cameraFoward = Vector3.Scale(cameraFoward, new Vector3(1, 0, 1));
            var cameraRight = Vector3.Scale(new Vector3(cameraFoward.z, 0, cameraFoward.x),new Vector3(1,0,-1));
            if (inputVec.magnitude != 0)
            {
                var moveDirection = cameraFoward * az + cameraRight * ax;
                CharacterMove(moveDirection);
            }
        }
    }
}

