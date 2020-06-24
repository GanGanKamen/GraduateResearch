using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum Type
    {
        Burst,  //最初期
        Tutorial, //チュートリアル用
        Experiment  //実験用
    }

    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject explosionEffect;
        public Type type;
        [SerializeField] private AudioClip beamClip;
        [SerializeField] private Transform playerTest;
        [SerializeField] private Transform head;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float searchDistance;
        [SerializeField] private float bulletSpeed;

        private float standbyCount = 0;
        private bool isAttack = false;

        private Experiment.ExperimentManager experimentManager;

        public float saveUntilDestoryTime = 0;

        public void Init(Vector3 _position,Transform _player, Experiment.ExperimentManager _mng,Type _type)
        {
            transform.position = _position;
            experimentManager = _mng;
            playerTest = _player;
            type = _type;
            if(type == Type.Experiment)
            {
                standbyCount = 2;
            }
        }

        void Update()
        {
            switch (type)
            {
                case Type.Burst:
                    BurstAction();
                    break;
                case Type.Tutorial:
                    LookAtTarget();
                    break;
                case Type.Experiment:
                    ExperimentAction();
                    break;
            }
        }

        private void BurstAction()
        {
            var dis = Vector3.Distance(playerTest.position, transform.position);
            if(dis <= searchDistance)
            {
                head.LookAt(playerTest.position + Vector3.up);
                if(standbyCount >= 1)
                {
                    standbyCount = 0;
                    StartCoroutine(Shoot(3,0.2f));
                }
                else
                {
                    if(isAttack == false)standbyCount += Time.deltaTime;
                }
                
            }           
        }

        private void ExperimentAction()
        {
            if(standbyCount >= 2)
            {
                standbyCount = 0;
                experimentManager.GetDamageFromEnemy(transform);
                LookAtTarget();
            }
            else
            {
                standbyCount += Time.deltaTime;
            }

            saveUntilDestoryTime += Time.deltaTime;
        }

        private void LookAtTarget()
        {
            head.LookAt(playerTest.position + Vector3.up);
        }

        private IEnumerator OneShoot(float intervalTime)
        {
            if (isAttack) yield break;
            isAttack = true;

            yield return new WaitForSeconds(intervalTime);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Attack"))
            {
                switch (type)
                {
                    default:
                        break;
                    case Type.Tutorial:
                        experimentManager.GetEnemyDestory(this);
                        break;
                    case Type.Experiment:
                        experimentManager.GetEnemyDestory(this,saveUntilDestoryTime);
                        break;
                }
                Instantiate(explosionEffect, transform.position, Quaternion.identity);               
                Destroy(gameObject);
            }
        }
    }
}

