using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject EnemyPrefab;
        [SerializeField] private float period;
        private bool startLoop = false;
        private List<EnemyPoint> enemyPoints;
        private float count = 0;
        // Start is called before the first frame update
        void Start()
        {
            enemyPoints = new List<EnemyPoint>();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("EnemyPoint"))
            {
                var ep = obj.GetComponent<EnemyPoint>();
                enemyPoints.Add(ep);
            }

        }

        // Update is called once per frame
        void Update()
        {
            InstanceLoop();
        }

        public void FirstInstance()
        {
            EnemyInstance(enemyPoints[0]);
            EnemyInstance(enemyPoints[2]);
            wall.SetActive(true);
            startLoop = true;
        }

        public void EnemyDead(EnemyPoint point)
        {
            for(int i = 0; i < enemyPoints.Count; i++)
            {
                if(enemyPoints[i] == point)
                {
                    //enemyPoints[i].IsApp = false;
                }
            }
        }

        private void InstanceLoop()
        {
            if (count < period)
            {
                count += Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < enemyPoints.Count; i++)
                {
                    if (enemyPoints[i].IsApp == false)
                    {
                        EnemyInstance(enemyPoints[i]);
                    }
                }
                count = 0;
            }
        }

        private void EnemyInstance(EnemyPoint point)
        {
            var enemyObj = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity);
            var enemyAI = enemyObj.GetComponent<AIManager>();
            //point.IsApp = true;
            enemyAI.Init(point);
        }
    }
}

