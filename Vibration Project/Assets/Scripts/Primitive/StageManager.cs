using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitive
{
    public enum StageMode
    {
        None,
        Vib,
        VibHeat
    }

    public class StageManager : MonoBehaviour
    {
        [SerializeField] private StageMode mode;
        [SerializeField] private AiManager frontEnemy;
        [SerializeField] private float period;
        [SerializeField] private float waitTime;
        [SerializeField] private GameObject[] enemysOnStage;
        [SerializeField] private HitManager hitManager;
        private MainPlayer player;
        private List<AiManager> enemys;
        private float timer = 0;

        private bool isWait = false;
        private bool isStart = false;
        private VestManager vestManager;
        // Start is called before the first frame update
        void Start()
        {
            switch (mode)
            {
                case StageMode.None:
                    Init();
                    hitManager.Init(player);
                    break;
                default:
                    StartCoroutine(VestInit());
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isStart)
            {
                timer += Time.deltaTime;
                Wait();
                EnemysUpdate();
                PlayerUpdate();
            }
        }

        public void Init()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
            player.Init(this);
            EnemysInit();
            if (waitTime > 0) isWait = true;
            isStart = true;
        }

        public void PlayGetDamage(Transform target)
        {
            var targetPos = new Vector2(target.position.x, target.position.z);
            var playerPos = new Vector2(player.transform.position.x,
                player.transform.position.z);
            float dx = targetPos.x - playerPos.x;
            float dy = targetPos.y - playerPos.y;
            float rad = Mathf.Atan2(dy, dx);
            var angle = rad * Mathf.Rad2Deg + player.transform.eulerAngles.y - 90f;
            switch (mode)
            {
                case StageMode.None:
                    hitManager.GetDamege(target, HP_Status.Pinch);
                    break;
                case StageMode.Vib:
                    break;
                case StageMode.VibHeat:
                    break;
            }
        }

        private IEnumerator VestInit()
        {
            vestManager = GameObject.Find("Vestmanager").GetComponent<VestManager>();
            vestManager.Init();
            if(mode == StageMode.VibHeat)
            {
                vestManager.SerialPort.ReadCompleteEventObject.AddListener(vestManager.ReadComplateString);
            }
            while(vestManager.HasSetGyiro == false)
            {
                yield return null;
            }
            Init();
        }

        private void EnemysInit()
        {
            for (int i = 0; i < enemysOnStage.Length; i++)
            {
                enemysOnStage[i].SetActive(true);
            }
            var epList = new List<AiManager>();
            foreach (GameObject obj in enemysOnStage)
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

        private void PlayerUpdate()
        {
            switch (mode)
            {
                case StageMode.None:
                    player.KeyCtrlTest();
                    break;
                default:
                    break;
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

