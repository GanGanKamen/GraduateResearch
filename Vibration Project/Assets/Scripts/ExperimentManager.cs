using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Experiment
{
    public class ExperimentManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;
        [SerializeField] private UI.DamageCanvas damageCanvas;
        [SerializeField] private Interface.VibrationSystem vibrationSystem;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField]private Phase phase = Phase.Tutorial;
        [SerializeField] private UnityEngine.UI.Text message;
        private List<Enemy.EnemyBase> nowEnemies = new List<Enemy.EnemyBase>();
        private float destoryEnemyTimer = 0;

        private List<float> normalDatas = new List<float>();
        private List<float> vibrateDatas = new List<float>();
        private List<float> extramDatas = new List<float>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ExperimentCoroutine());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator ExperimentCoroutine()
        {
            for(int i = 0;i < 3; i++)
            {
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                var pos = Vector3.zero;
                switch (i)
                {
                    case 0:
                        pos = new Vector3(distanceFromPlayer, 0, 0);
                        break;
                    case 1:
                        pos = new Vector3(distanceFromPlayer * Mathf.Cos(45f) * -1, 0, -distanceFromPlayer * Mathf.Sin(45f));
                        break;
                    case 2:
                        pos = new Vector3(0, 0, distanceFromPlayer);
                        break;
                }
                var enemyBase = enemy.GetComponent<Enemy.EnemyBase>();
                enemyBase.Init(pos, player, this, Enemy.Type.Tutorial);
                nowEnemies.Add(enemyBase);
            }

            while (nowEnemies.Count > 0)
            {
                message.text = "すべての目標を撃破せよ！" + "\n" + nowEnemies.Count + " / 3";
                yield return null;
            }

            message.text = "Next!";
            yield return new WaitForSeconds(1f);
            message.text = "";

            phase = Phase.Extream;

            for (int i = 0;i< 10; i++)
            {
                var waitTime = Random.Range(2f, 5f);
                yield return new WaitForSeconds(waitTime);
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                var pos = CreatePosition();
                var enemyBase = enemy.GetComponent<Enemy.EnemyBase>();
                enemyBase.Init(pos, player, this, Enemy.Type.Experiment);
                nowEnemies.Add(enemyBase);
                while (nowEnemies.Count > 0) yield return null;
                normalDatas.Add(destoryEnemyTimer);
                Debug.Log("normalDatas:" + i + " " + normalDatas[i]);
            }

            phase = Phase.Vibrate;
        }

        private int PlusOrMinus()
        {
            var result = Random.Range(-1, 2);
            while(result == 0)
            {
                result = Random.Range(-1, 2);
            }
            return result;
        }

        private Vector3 CreatePosition()
        {

            float angle = Random.Range(0, 360);
            var directionX = PlusOrMinus();
            var posX = Mathf.Cos(angle) * distanceFromPlayer * directionX;
            var directionZ = PlusOrMinus();
            var posZ = Mathf.Sin(angle) * distanceFromPlayer * directionZ;
            var result = new Vector3(posX, 0, posZ);

            return result;
        }

        private float CenterAngle(Transform target)
        {
            var targetPos = new Vector2(target.position.x, target.position.z);
            var playerPos = new Vector2(player.position.x, player.position.z);
            float dx = targetPos.x - playerPos.x;
            float dy = targetPos.y - playerPos.y;
            float rad = Mathf.Atan2(dy, dx);
            return rad * Mathf.Rad2Deg + player.eulerAngles.y - 90f; //angleRange:-180 ~ 180
        }

        public void GetDamageFromEnemy(Transform target)
        {
            switch (phase)
            {
                default:
                    break;
                case Phase.Normal:
                    damageCanvas.SetMarkActive(target);
                    break;
                case Phase.Vibrate:
                    vibrationSystem.PlayVibration(CenterAngle(target));
                    break;
                case Phase.Extream:
                    damageCanvas.SetMarkActive(target);
                    vibrationSystem.PlayVibration(CenterAngle(target));
                    break;
            }
        }

        public void GetEnemyDestory(Enemy.EnemyBase enemy, float saveTime)
        {
            if (nowEnemies.Count <= 0) return;
            destoryEnemyTimer = saveTime;
            nowEnemies.Remove(enemy);
        }

        public void GetEnemyDestory(Enemy.EnemyBase enemy)
        {
            if (nowEnemies.Count <= 0) return;
            nowEnemies.Remove(enemy);
        }
    }
}


