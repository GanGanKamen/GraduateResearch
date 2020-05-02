using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float shootCoolDownTime;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Transform gun;
        private bool isAttack = false;
        private float shootCoolDownCount = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ShootOver();
            }
        }

        private void Shoot()
        {
            //StartCoroutine(ShootCoroutine());
            if(shootCoolDownCount >= shootCoolDownTime)
            {
                shootCoolDownCount = 0;
                GameObject bulletObj = Instantiate(bullet, muzzle.position, Quaternion.identity);
                var foward = muzzle.position - gun.position;
                bulletObj.GetComponent<Attack.Bullet>().Init(muzzle.position + foward, bulletSpeed, this.transform);
            }
            else
            {
                shootCoolDownCount += Time.deltaTime;
            }
        }

        private void ShootOver()
        {
            shootCoolDownCount = 0;
        }

        private IEnumerator ShootCoroutine()
        {
            if (isAttack) yield break;
            isAttack = true;
            GameObject bulletObj = Instantiate(bullet, muzzle.position, Quaternion.identity);
            var foward = muzzle.position - gun.position;
            bulletObj.GetComponent<Attack.Bullet>().Init(muzzle.position + foward, bulletSpeed, this.transform);
            yield return new WaitForSeconds(shootCoolDownTime);
            isAttack = false;
            yield break;
        }
    }
}

