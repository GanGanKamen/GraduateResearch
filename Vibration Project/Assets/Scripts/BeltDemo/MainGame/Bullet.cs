﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attack
{
    public class Bullet : MonoBehaviour
    {
        public Transform master;
        private Vector3 goalPosition;
        private float speed;

        public void Init(Vector3 _goalPos ,float _speed,Transform _master)
        {
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
            Destroy(gameObject, 3f);
        }
    }
}

