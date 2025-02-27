﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDamage : MonoBehaviour
    {
        [SerializeField] private AudioClip hitAudio;
        [SerializeField] private UI.DamageCanvas damageCanvas;
        [SerializeField] private Interface.VibrationSystem vibrationSystem;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HitBullet(Attack.Bullet bullet)
        {
            if (damageCanvas != null) damageCanvas.SetMarkActive(bullet.master,true);
            if (vibrationSystem != null) vibrationSystem.PlayVibration(damageCanvas.MarkAngle);
            GetComponent<AudioSource>().PlayOneShot(hitAudio);
            Destroy(bullet.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Attack"))
            {
                var bullet = other.GetComponent<Attack.Bullet>();
                if (bullet.master != transform)
                {
                    HitBullet(other.GetComponent<Attack.Bullet>());
                }

            }
        }
    }
}

