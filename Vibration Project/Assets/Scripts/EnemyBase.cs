using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public GameObject bullet;
        [SerializeField] private Transform playerTest;
        [SerializeField] private Transform head;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float searchDistance;
        [SerializeField] private float bulletSpeed;
        private float standbyCount = 0;
        private float standbyTime = 1;
        private bool isAttack = false;
        void Start()
        {
            playerTest = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            LookAtTarget();
        }

        private void LookAtTarget()
        {
            var dis = Vector3.Distance(playerTest.position, transform.position);
            if(dis <= searchDistance)
            {
                head.LookAt(playerTest.position + Vector3.up);
                if(standbyCount >= standbyTime)
                {
                    standbyCount = 0;
                    StartCoroutine(Shoot(3,0.2f));
                }
                else
                {
                    if(isAttack == false)standbyCount += Time.deltaTime;
                }
                
            }
            else
            {
                
            }
            
        }

        private IEnumerator Shoot(int shootNum, float intervalTime)
        {
            if (isAttack) yield break;
            isAttack = true;
            for(int i = 0; i < shootNum; i++)
            {
                GameObject bulletObj = Instantiate(bullet, muzzle.position, Quaternion.identity);
                bulletObj.GetComponent<Attack.Bullet>().Init(playerTest.position,bulletSpeed,this);
                yield return new WaitForSeconds(intervalTime);
            }
            isAttack = false;
            yield break;
        }
    }
}

