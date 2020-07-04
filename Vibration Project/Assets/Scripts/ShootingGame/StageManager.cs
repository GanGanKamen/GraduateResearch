using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private GameObject EnemyPrefab;
        private List<EnemyPoint> enemyPoints;
        // Start is called before the first frame update
        void Start()
        {
            enemyPoints = new List<EnemyPoint>();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("EnemyPoint"))
            {
                var ep = obj.GetComponent<EnemyPoint>();
                enemyPoints.Add(ep);
            }

            for(int i = 0; i < enemyPoints.Count; i++)
            {
                EnemyInstance(enemyPoints[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void EnemyInstance(EnemyPoint point)
        {
            var enemyObj = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity);
            var enemyAI = enemyObj.GetComponent<AIManager>();
            enemyAI.Init(point.FirstPosition, point.SecondPosition);
        }
    }
}

