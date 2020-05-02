using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Burst,  //最初期
        Normal  //実験用
    }

    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        public Type type;
        [SerializeField] private LineRenderer myBeam;
        [SerializeField] private AudioClip beamClip;
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
            switch (type)
            {
                case Type.Burst:
                    LookAtTarget();
                    break;
                case Type.Normal:
                    break;
            }
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
        private IEnumerator OneShoot(float intervalTime)
        {
            if (isAttack) yield break;
            GetComponent<AudioSource>().PlayOneShot(beamClip);
            isAttack = true;
            myBeam.gameObject.SetActive(true);
            myBeam.SetPosition(1, playerTest.transform.position + Vector3.up);
            yield return new WaitForSeconds(intervalTime);
            myBeam.gameObject.SetActive(false);
            isAttack = false;
            yield break;
        }

        private IEnumerator Shoot(int shootNum, float intervalTime)
        {
            if (isAttack) yield break;
            GetComponent<AudioSource>().PlayOneShot(beamClip);
            isAttack = true;
            for(int i = 0; i < shootNum; i++)
            {
                GameObject bulletObj = Instantiate(bullet, muzzle.position, Quaternion.identity);
                bulletObj.GetComponent<Attack.Bullet>().Init(playerTest.position,bulletSpeed,this.transform);
                yield return new WaitForSeconds(intervalTime);
            }
            isAttack = false;
            yield break;
        }
    }
}

