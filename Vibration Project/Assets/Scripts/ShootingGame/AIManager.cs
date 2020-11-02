using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shooting
{
    public enum AIStatus
    {
        Standby,
        Approach,
        Attack,
        Run
    }

    public class AIManager : SoldierBase
    {
        public EnemyPoint InstancePoint { get { return instancePoint; } }
        [SerializeField]private AIStatus status = AIStatus.Standby;
        private Transform target;
        private Vector3 firstPosition;
        private Vector3 secordPosition;
        private EnemyPoint instancePoint;
        private float saveTime;
        private float saveTimeCount;
        [SerializeField] private EnemyPoint testEnemyPoint;

        private float coolCount = 0;

        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (status)
            {
                case AIStatus.Approach:
                    GoToSecord();
                    break;
                case AIStatus.Attack:
                    LookAtTarget();
                    if (AttackCooling() == true) return;
                    Shoot();
                    StaySave();
                    break;
                case AIStatus.Run:
                    GoToFirst();
                    break;
            }

        }

        public void Init(EnemyPoint _enemyPoint)
        {
            instancePoint = _enemyPoint;
            transform.position = _enemyPoint.FirstPosition;
            firstPosition = _enemyPoint.transform.position;
            secordPosition = _enemyPoint.SecondPosition;
            saveTime = _enemyPoint.saveTime;
            status = AIStatus.Approach;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            iK.SetError(_enemyPoint.aimErrorHorizon, _enemyPoint.aimErrorHight);
            Init(Character.Enemy, BulletMethod.Bullet);
        }

        public void Run()
        {
            if (status == AIStatus.Run) return;
            saveTimeCount = 0;
            ShootOver();
            CancelAiming();
            status = AIStatus.Run;
        }

        private void GoToSecord()
        {
            var direction = Vector3.Scale(
                (secordPosition - transform.position),new Vector3(1,0,1));
            CharacterMove(direction);

            var pos0 = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
            var pos1 = Vector3.Scale(secordPosition, new Vector3(1, 0, 1));
            var dis = Vector3.Distance(pos0,pos1);
            if(dis <= 0.1f)
            {
                CharacterStand();
                SetAiming();
                status = AIStatus.Attack;
            }
        }

        private void GoToFirst()
        {
            var direction = Vector3.Scale(
                (firstPosition - transform.position), new Vector3(1, 0, 1));
            CharacterMove(direction);
            var pos0 = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
            var pos1 = Vector3.Scale(firstPosition, new Vector3(1, 0, 1));
            var dis = Vector3.Distance(pos0, pos1);
            if (dis <= 0.5f)
            {
                CharacterStand();
                instancePoint.EnemyDestory();
                Destroy(gameObject);
            }
        }

        private void LookAtTarget()
        {
            var dir = Vector3.Scale((target.transform.position - transform.position),
                new Vector3(1, 0, 1));
            SetTransformRotation(dir.normalized);
            SetAimHight(target);
            /*
            var hight = Mathf.Abs(transform.position.y - target.transform.position.y);
            if (hight >= 0.5f && hasSetHight == false)
            {
                SetAimHight();
                hasSetHight = true;
                Debug.Log(target.transform.position.y - transform.position.y);
            }
            */
        }

        private bool AttackCooling()
        {
            if(coolCount < 1)
            {
                coolCount += Time.deltaTime;
                if(coolCount >= 1)
                {
                    coolCount = 1;
                }
                return true;
            }

            else
            {
                return false;
            }
        }

        private void StaySave()
        {
            if (saveTime <= 0) return;
            if(saveTimeCount < saveTime )
            {
                saveTimeCount += Time.deltaTime;
            }
            else
            {
                if(status != AIStatus.Run)
                {
                    Run();
                }
            }
            if(target.GetComponent<MainPlayer>().Dead)Run();
        }
    }
}

