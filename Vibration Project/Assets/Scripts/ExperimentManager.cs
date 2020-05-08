using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

        private List<float> normalAngles = new List<float>();
        private List<float> vibrateAngles = new List<float>();
        private List<float> extramAngles = new List<float>();
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

            message.text = "Next! Normal";
            yield return new WaitForSeconds(1f);
            message.text = "";

            phase = Phase.Normal;

            for (int i = 0;i< 10; i++)
            {
                var waitTime = Random.Range(2f, 5f);
                yield return new WaitForSeconds(waitTime);
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                var pos = CreatePosition();
                var enemyBase = enemy.GetComponent<Enemy.EnemyBase>();
                enemyBase.Init(pos, player, this, Enemy.Type.Experiment);
                nowEnemies.Add(enemyBase);
                var preAngle = player.eulerAngles.y;
                while (nowEnemies.Count > 0) yield return null;
                normalDatas.Add(destoryEnemyTimer);
                var changedAngle = Mathf.Abs(preAngle - player.eulerAngles.y);
                normalAngles.Add(changedAngle);
            }

            message.text = "Next! Vibrate";
            yield return new WaitForSeconds(1f);
            message.text = "";
            phase = Phase.Vibrate;

            for (int i = 0; i < 10; i++)
            {
                var waitTime = Random.Range(2f, 5f);
                yield return new WaitForSeconds(waitTime);
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                var pos = CreatePosition();
                var enemyBase = enemy.GetComponent<Enemy.EnemyBase>();
                enemyBase.Init(pos, player, this, Enemy.Type.Experiment);
                nowEnemies.Add(enemyBase);
                var preAngle = player.eulerAngles.y;
                while (nowEnemies.Count > 0) yield return null;
                vibrateDatas.Add(destoryEnemyTimer);
                var changedAngle = Mathf.Abs(preAngle - player.eulerAngles.y);
                vibrateAngles.Add(changedAngle);
            }

            message.text = "Next! Extream";
            yield return new WaitForSeconds(1f);
            message.text = "";
            phase = Phase.Extream;

            for (int i = 0; i < 10; i++)
            {
                var waitTime = Random.Range(2f, 5f);
                yield return new WaitForSeconds(waitTime);
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                var pos = CreatePosition();
                var enemyBase = enemy.GetComponent<Enemy.EnemyBase>();
                enemyBase.Init(pos, player, this, Enemy.Type.Experiment);
                nowEnemies.Add(enemyBase);
                var preAngle = player.eulerAngles.y;
                while (nowEnemies.Count > 0) yield return null;
                extramDatas.Add(destoryEnemyTimer);
                var changedAngle = Mathf.Abs(preAngle - player.eulerAngles.y);
                extramAngles.Add(changedAngle);
            }

            message.text = "Clear!";
            ResultTextOutput();
        }


        private void ResultTextOutput()
        {
            var info = System.GameSystem.baseInfo;
            FileInfo fileInfo = new FileInfo(Application.dataPath + "/" + info.playerName + "_Log.txt");
            StreamWriter writer = fileInfo.AppendText();
            var nameText = "名前：" + info.playerName + "\n";
            var sexText = "性別：";
            switch (info.sex)
            {
                case Player.BaseInfo.SexGroup.Man:
                    sexText = sexText + "男" + "\n";
                    break;
                case Player.BaseInfo.SexGroup.Woman:
                    sexText = sexText + "女" + "\n";
                    break;
                case Player.BaseInfo.SexGroup.Other:
                    sexText = sexText + "その他" + "\n";
                    break;
            }
            var ageText = "年齢：" + info.age + "\n";
            var skillText = "FPSゲーム経験：";
            switch (info.playerSkill)
            {
                case 0:
                    skillText = skillText + "未経験" + "\n";
                    break;
                case 1:
                    skillText = skillText + "初心者" + "\n";
                    break;
                case 2:
                    skillText = skillText + "中級者" + "\n";
                    break;
                case 3:
                    skillText = skillText + "上級者" + "\n";
                    break;
            }

            var introduction = nameText + sexText + ageText + skillText;

            var normalLog = "\n" + "Normal" + "\n";
            for(int i = 0; i < normalDatas.Count; i++)
            {
                var reactionSpeed = normalAngles[i] / normalDatas[i];
                normalLog = normalLog + i + " : " + normalDatas[i] + ","
                    + normalAngles[i] + "," + reactionSpeed + "\n";
            }

            var vibrateLog = "\n" + "Vibrate" + "\n";
            for (int i = 0; i < vibrateDatas.Count; i++)
            {
                var reactionSpeed = vibrateAngles[i] / vibrateDatas[i];
                vibrateLog = vibrateLog + i + " : " + vibrateDatas[i] + ","
                    + vibrateAngles[i] + "," + reactionSpeed + "\n";
            }

            var extreamLog = "\n" + "Extream" + "\n";
            for (int i = 0; i < extramDatas.Count; i++)
            {
                var reactionSpeed = extramAngles[i] / extramDatas[i];
                extreamLog = extreamLog + i + " : " + extramDatas[i] + ","
                    + extramAngles[i] + "," + reactionSpeed + "\n";
            }

            var logs = normalLog + vibrateLog + extreamLog;
            writer.WriteLine(introduction + logs);
            writer.Flush();
            writer.Close();            
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


