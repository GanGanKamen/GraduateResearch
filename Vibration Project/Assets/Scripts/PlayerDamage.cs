using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerDamage : MonoBehaviour
    {
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
            if(damageCanvas != null)damageCanvas.SetMarkActive(bullet.master.transform);
            if (vibrationSystem != null) vibrationSystem.PlayVibration(damageCanvas.MarkAngle);
            Destroy(bullet.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Attack"))
            {
                HitBullet(other.GetComponent<Attack.Bullet>());
            }
        }
    }
}

