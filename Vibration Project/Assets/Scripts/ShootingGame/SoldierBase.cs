using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class SoldierBase : MonoBehaviour
    {
        public GameObject Body { get { return body; } }
        public Vector3 forwardVec { get { return (front.position - transform.position).normalized; } }
        public Vector3 rightVec { get { return (right.position - transform.position).normalized; } }
        public bool IsAim { get { return isAim; } }
        [SerializeField] private float runSpeed;
        [SerializeField] private GameObject body;
        [SerializeField] private Animator soldierAnimator;
        [SerializeField] private Transform front;
        [SerializeField] private Transform right;
        [SerializeField]private bool isAim = false;
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

        public void Aiming(bool _isAim)
        {
            switch (_isAim)
            {
                case true:
                    isAim = true;
                    //SetAimRotation();
                    soldierAnimator.SetBool("IsAim", true);
                    break;
                case false:
                    isAim = false;
                    ResetRotation();
                    soldierAnimator.SetBool("IsAim", false);
                    break;
            }
        }

        public void AimMove(Vector2 inputVec)
        {
            if (isAim == false) return;
            var moveVec = forwardVec * inputVec.y + rightVec * inputVec.x;
            transform.position += moveVec * Time.deltaTime * runSpeed * 0.6f;
            Debug.Log(moveVec);
            soldierAnimator.SetFloat("Move_X", inputVec.x);
            soldierAnimator.SetFloat("Move_Y", inputVec.y);
        }

        public void AimRotate(float horizontal, float vertical)
        {
            if (isAim == false) return;
            transform.localEulerAngles += new Vector3(0,horizontal,0);
            var direction = Vector3.Scale(transform.forward, new Vector3(1, 0, 1));
            transform.localRotation = Quaternion.LookRotation(direction);
            body.transform.localEulerAngles = Vector3.zero;
        }
    }
}

