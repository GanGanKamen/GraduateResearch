using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitive
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private AiManager frontEnemy;
        [SerializeField] private float period;

        private MainPlayer player;
        private List<AiManager> enemys;
        [SerializeField] private float timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
            var epList = new List<AiManager>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var ep = obj.GetComponent<AiManager>();
                if (ep != frontEnemy) epList.Add(ep);
            }
            var enemyArray = epList.ToArray();
            enemyArray = EnemyShuffle(enemyArray);
        }

        // Update is called once per frame
        void Update()
        {

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

