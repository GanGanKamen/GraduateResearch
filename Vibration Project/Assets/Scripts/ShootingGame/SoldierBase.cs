using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class SoldierBase : MonoBehaviour
    {
        public GameObject Body { get { return body; } }
        public GameObject Weapon { get { return weapon; } }
        public bool IsAim { get { return isAim; } }
        [SerializeField] private float runSpeed;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject weapon;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Animator soldierAnimator;
        [SerializeField] private bool isAim = false;
        [SerializeField] private IKManager iK;
        [SerializeField] private Vector2 aimAngleLimit;
        [SerializeField] private float aimAngle = 0;

        private float defaultWeaponAngle;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CharacterMove(Vector3 _direction)
        {
            if (isAim) return;
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            body.transform.localRotation = Quaternion.LookRotation(direction);
            var move = Mathf.Abs(_direction.x) + Mathf.Abs(_direction.z);
            if (move > 1) move = 1;
            transform.Translate(direction * Time.deltaTime * runSpeed * move);
            soldierAnimator.SetFloat("Move", move);
        }

        public void CharacterStand()
        {
            soldierAnimator.SetFloat("Move", 0);
            soldierAnimator.SetFloat("Move_X", 0);
            soldierAnimator.SetFloat("Move_Y", 0);
        }

        public void SetTransformRotation(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.localRotation = Quaternion.LookRotation(direction);
            body.transform.localEulerAngles = Vector3.zero;
        }

        public void SetBodyRotation(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.localEulerAngles = Vector3.zero;
            body.transform.localRotation = Quaternion.LookRotation(direction);
        }

        public void ResetRotation()
        {
            var rot = transform.localEulerAngles;
            transform.localEulerAngles = Vector3.zero;
            body.transform.localEulerAngles = rot;
        }

        public void SetAimRotation()
        {
            var rot = body.transform.localEulerAngles;
            body.transform.localEulerAngles = Vector3.zero;
            transform.localEulerAngles = rot;
        }

        public void SetAiming(Vector3 vec)
        {
            isAim = true;
            //SetAimRotation();
            aimAngle = 0;
            iK.SetIK();
            soldierAnimator.SetBool("IsAim", true);
            defaultWeaponAngle = weapon.transform.localEulerAngles.y;
        }

        public void CancelAiming()
        {
            isAim = false;
            ResetRotation();
            aimAngle = 0;
            iK.ResetIK();
            soldierAnimator.SetBool("IsAim", false);
        }

        public void AimMove(Vector2 inputVec)
        {
            if (isAim == false) return;
            var moveVec = transform.forward * inputVec.y + transform.right * inputVec.x;
            transform.position += moveVec * Time.deltaTime * runSpeed * 0.6f;
            soldierAnimator.SetFloat("Move_X", inputVec.x);
            soldierAnimator.SetFloat("Move_Y", inputVec.y);
        }

        public void AimRotate(float horizontal, float vertical)
        {
            if (isAim == false) return;
            transform.localEulerAngles += new Vector3(0,horizontal * Time.deltaTime * 360f,0);
            var direction = Vector3.Scale(transform.forward, new Vector3(1, 0, 1));
            transform.localRotation = Quaternion.LookRotation(direction);
            body.transform.localEulerAngles = Vector3.zero;

            
            aimAngle += vertical * Time.deltaTime * 360f;

            if (aimAngle > aimAngleLimit.y) aimAngle = aimAngleLimit.y;
            else if (aimAngle < aimAngleLimit.x) aimAngle = aimAngleLimit.x;
                
        }

        public void AimCameraRotate(float horizontal, Vector3 vec)
        {
            if (isAim == false) return;
            transform.localEulerAngles += new Vector3(0, horizontal * Time.deltaTime * 360f, 0);
            var direction = Vector3.Scale(transform.forward, new Vector3(1, 0, 1));
            transform.localRotation = Quaternion.LookRotation(direction);
            body.transform.localEulerAngles = Vector3.zero;


            iK.SetTargetVec(vec);
        }
    }
}

