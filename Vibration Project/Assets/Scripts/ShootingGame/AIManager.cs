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
        [SerializeField]private AIStatus status = AIStatus.Standby;
        private SoldierBase target;
        private Vector3 firstPosition;
        private Vector3 secordPosition;

        // Start is called before the first frame update
        void Start()
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
                    Shoot();
                    break;
            }

        }

        public void Init(Vector3 _first,Vector3 _secord)
        {            
            transform.position = _first;
            firstPosition = transform.position;
            secordPosition = _secord;
            status = AIStatus.Approach;
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierBase>();
        }

        private void GoToSecord()
        {
            var direction = Vector3.Scale(
                (secordPosition - transform.position),new Vector3(1,0,1));
            CharacterMove(direction);

            var dis = Vector3.Distance(transform.position, secordPosition);
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
            
        }
    }
}

