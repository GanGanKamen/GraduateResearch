using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shooting
{
    public enum AIStatus
    {
        Standby,
        Approach,
        Attack
    }

    public class AIManager : SoldierBase
    {
        public EnemyPoint InstancePoint { get { return instancePoint; } }
        [SerializeField]private AIStatus status = AIStatus.Standby;
        private Transform target;
        private Vector3 firstPosition;
        private Vector3 secordPosition;
        private EnemyPoint instancePoint;
        private bool hasSetHight = false;
        [SerializeField] private EnemyPoint testEnemyPoint;

        private void Start()
        {
            Init(testEnemyPoint);
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
                    Shoot();
                    break;
            }

        }

        public void Init(EnemyPoint _enemyPoint)
        {
            instancePoint = _enemyPoint;
            transform.position = _enemyPoint.FirstPosition;
            firstPosition = transform.position;
            secordPosition = _enemyPoint.SecondPosition;
            status = AIStatus.Approach;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Init(Character.Enemy);
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
    }
}

