﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

public class BulletMain : MonoBehaviour
{
    public SoldierBase Master { get { return master; } }
    public Vector3 StartPosition { get { return startPos; } }
    public bool HasHit { get { return hasHit; } }
    private Vector3 startPos;
    private Vector3 goalPosition;
    private float speed;
    private SoldierBase master;
    private bool hasHit = false;
    private float count = 0;
    [SerializeField] private GameObject hitEffect;

    public void Init(Vector3 _goalPos, float _speed, SoldierBase _master)
    {
        startPos = transform.position;
        goalPosition = _goalPos;
        speed = _speed;
        master = _master;
        transform.LookAt(goalPosition);
        gameObject.AddComponent<Rigidbody>();
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        var force = _goalPos - transform.position;
        rb.AddForce(force * speed, ForceMode.Acceleration);
    }

    private void Update()
    {
        if (count < 3)
        {
            count += Time.deltaTime;
        }
        else
        {
            if (hasHit == false) hasHit = true;
        }

        if (hasHit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasHit) return;
            var player = other.GetComponent<MainPlayer>();
            player.GetDamege(master.transform);
        }

        else if (other.CompareTag("Stage"))
        {
            var hitObj = Instantiate(hitEffect, transform.position, Quaternion.identity);
            hitObj.transform.LookAt(startPos);
            hasHit = true;
        }
    }
}