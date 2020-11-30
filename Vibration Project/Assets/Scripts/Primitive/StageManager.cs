using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private AudioClip voice1;
        [SerializeField] private AudioClip voice2;
        private MainPlayer player;
        private List<AiManager> enemys;
        private float timer = 0;

        private bool isWait = false;
        private bool isStart = false;
        private VestManager vestManager;
        private AudioSource audioSource;
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
            var playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
            var vec0 = player.FrontVec2D;
            var vec1 = (targetPos - playerPos).normalized;
            var angle = Vector2.Angle(vec0, vec1);
            var cross = vec0.x * vec1.y - vec0.y * vec1.x;
            if (cross > 0) angle = 360 -angle;
            switch (mode)
            {
                case StageMode.None:
                    hitManager.GetDamege(target, HP_Status.Pinch);
                    break;
                case StageMode.Vib:
                    var dir = vestManager.AngleToHitDirection(angle);
                    //vestManager.StopAllHit();
                    //vestManager.StartHit(dir);
                    vestManager.OneHit(dir);
                    break;
                case StageMode.VibHeat:
                    var dir0 = vestManager.AngleToHitDirection(angle);
                    vestManager.StartBlood(dir0);
                    //vestManager.StopAllHit();
                    // vestManager.StartHit(dir0);
                    vestManager.OneHit(dir0);
                    break;
            }
        }

        public void PlayerDie()
        {
            for(int i = 0; i < enemys.Count; i++)
            {
                enemys[i].AttackCancel();
            }
            if(vestManager != null)
            {
                vestManager.DisConect();
            }

            Fader.FadeInBlack(2, "Dead");
        }

        private IEnumerator VestInit()
        {
            vestManager = GameObject.Find("VestManager").GetComponent<VestManager>();
            vestManager.Init();
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(voice1);
            while (vestManager.HasSetGyiro == false)
            {
                yield return null;
            }
            audioSource.PlayOneShot(voice2);
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
            if (isWait || player.Dead) return;
            for(int i = 0; i < enemys.Count; i++)
            {
                enemys[i].StatusUpdate();
                if(timer - waitTime >= period * i)
                {
                    if(enemys[i].IsAttack == false )
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
                    player.BodyRotate(vestManager.GyiroInput);
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

