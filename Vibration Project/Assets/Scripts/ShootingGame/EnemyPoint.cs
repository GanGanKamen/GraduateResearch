using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class EnemyPoint : MonoBehaviour
    {
        public AIManager Enemy { get { return _enemy; } }
        public Vector3 FirstPosition { get { return transform.position; } }
        public Vector3 SecondPosition { get { return second.position; } }
        public bool IsApp { get { return _isApp; } }
        public float saveTime;
        public float aimErrorHorizon;
        public float aimErrorHight;

        private bool _isApp = false;
        private AIManager _enemy;
        [SerializeField] private Transform second;
        [SerializeField] private GameObject EnemyPrefab;

        public void EnemyInstance()
        {
            if (_isApp) return;
            var enemyObj = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity);
            var enemyAI = enemyObj.GetComponent<AIManager>();
            _enemy = enemyAI;
            _isApp = true;
            enemyAI.Init(this);
        }

        public void EnemyDestory()
        {
            if (_isApp == false) return;
            _isApp = false;
        }
    }
}

