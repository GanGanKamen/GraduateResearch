using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitive
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private AiManager frontEnemy;
        [SerializeField] private float period;
        [SerializeField] private float waitTime;
        private MainPlayer player;
        [SerializeField]private List<AiManager> enemys;
        [SerializeField] private float timer = 0;

        private bool isWait = false;
        private bool isStart = false;
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (isStart)
            {
                timer += Time.deltaTime;
                Wait();
                EnemysUpdate();
            }

        }

        public void Init()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
            EnemysInit();
            if (waitTime > 0) isWait = true;
            isStart = true;
        }

        private void EnemysInit()
        {
            var epList = new List<AiManager>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var ep = obj.GetComponent<AiManager>();
                if (ep != frontEnemy) epList.Add(ep);
            }
            var enemyArray = epList.ToArray();
            enemyArray = EnemyShuffle(enemyArray);

            enemys = new List<AiManager>();
            enemys.Add(enemyArray[0]);
            
            epList.Remove(enemyArray[0]);
            epList.Add(frontEnemy);
            var remainEnemys = epList.ToArray();
            remainEnemys = EnemyShuffle(remainEnemys);
            enemys.AddRange(remainEnemys);

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].Init(player.transform);
            }
        }

        private void Wait()
        {
            if (isWait)
            {
                if(timer >= waitTime)
                {
                    isWait = false;
                }
            }
        }

        private void EnemysUpdate()
        {
            if (isWait) return;
            for(int i = 0; i < enemys.Count; i++)
            {
                enemys[i].StatusUpdate();
                if(timer - waitTime >= period * i)
                {
                    if(enemys[i].IsAttack == false)
                    enemys[i].ToAttack();
                }
            }
        }

        private AiManager[] EnemyShuffle(AiManager[] array)
        {
            int length = array.Length;
            AiManager[] result = new AiManager[length];
            array.CopyTo(result, 0);

            for (int i = 0; i < length; i++)
            {
                AiManager tmp = result[i];
                int randomIndex = Random.Range(i, length);
                result[i] = result[randomIndex];
                result[randomIndex] = tmp;
            }

            return result;

        }
    }
}

