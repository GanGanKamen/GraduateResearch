using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class Bullet : MonoBehaviour
    {
        public SoldierBase Master { get { return master; } }
        public Vector3 StartPosition { get { return startPos; } }
        private Vector3 startPos;
        private Vector3 goalPosition;
        private float speed;
        private SoldierBase master;
        private bool hasHit = false;
        [SerializeField] private GameObject hitEffect;

        public void Init(Vector3 _goalPos, float _speed, SoldierBase _master)
        {
            startPos = transform.position;
            goalPosition = _goalPos;
            speed = _speed;
            master = _master;
            //var rotVec = (_goalPos - transform.position).normalized;
            transform.LookAt(goalPosition);
            gameObject.AddComponent<Rigidbody>();
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            var force = _goalPos - transform.position;
            rb.AddForce(force * speed, ForceMode.Acceleration);
        }

        private void Update()
        {
            if (hasHit)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {           
            if(other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                if (hasHit) return;
                var soldier = other.GetComponent<SoldierBase>();
                if(master != soldier)
                {
                    soldier.GetDamaged(2,startPos);
                    hasHit = true;
                }
            }

            else if (other.CompareTag("Stage"))
            {
                var hitObj = Instantiate(hitEffect, transform.position, Quaternion.identity);
                hitObj.transform.LookAt(startPos);
                hasHit = true;
            }
        }
    }
}

