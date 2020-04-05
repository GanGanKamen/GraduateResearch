using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerDamage : MonoBehaviour
    {
        [SerializeField] private UI.DamageCanvas damageCanvas;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HitBullet(Attack.Bullet bullet)
        {
            damageCanvas.SetMarkActive(bullet.master.transform);
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

