using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Primitive {

    public class AiManager : SoldierBase
    {
        public Transform Target { get { return _target; } }
        public bool IsAttack { get { return _isAttack; } }
        private bool _isAttack = false;
        private bool preAttack = false;
        private Transform _target;
        private float coolCount = 0;
        private bool complete = false;
        public void Init(Transform target)
        {
            _target = target;
        }

        public void StatusUpdate()
        {
            switch (_isAttack)
            {
                case false:
                    break;
                case true:
                    if (AttackCooling() == true) return;
                    Shoot();
                    break;
            }
        }

        public void ToAttack()
        {
            if (_isAttack || complete) return;
            Body.SetActive(true);
            CharacterStand();
            SetAiming();
            _isAttack = true;

        }

        public void AttackCancel()
        {
            if (_isAttack == false) return;
            ShootOver();
            CancelAiming();
            Body.SetActive(false);
            _isAttack = false;
            complete = true;
        }

        private void LookAtTarget()
        {
            var dir = Vector3.Scale((_target.transform.position - transform.position),
                new Vector3(1, 0, 1));
            SetTransformRotation(dir.normalized);
            SetAimHight(_target);

        }

        private bool AttackCooling()
        {
            if (coolCount < 1)
            {
                coolCount += Time.deltaTime;
                if (coolCount > 1)
                {
                    coolCount = 1;
                }
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}


