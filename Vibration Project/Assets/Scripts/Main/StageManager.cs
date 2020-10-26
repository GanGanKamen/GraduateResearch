using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Shooting.EnemyPoint frontPoint;
    [SerializeField] private float firstTime;
    [SerializeField] private float period;

    private MainPlayer player;
    private Shooting.EnemyPoint[] enemyPoints;
    [SerializeField]private float timer = 0;
    private bool firstHasRun = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        var epList = new List<Shooting.EnemyPoint>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EnemyPoint"))
        {
            var ep = obj.GetComponent<Shooting.EnemyPoint>();
            if (ep != frontPoint) epList.Add(ep);
        }
        enemyPoints = epList.ToArray();
        if(frontPoint != null) frontPoint.EnemyInstance();
        enemyPoints = PointsShuffle(enemyPoints);
        //TestAllIntance();
    }

    // Update is called once per frame
    void Update()
    {
        TimeLineUpdate();
    }

    private void TestAllIntance()
    {
        for (int i = 0; i < enemyPoints.Length; i++)
        {
            enemyPoints[i].EnemyInstance();
        }
    }

    private void TimeLineUpdate()
    {
        if (player.Dead) return;
        timer += Time.deltaTime;
        if (timer >= firstTime)
        {
            if (firstHasRun == false)
            {
                firstHasRun = true;
                frontPoint.Enemy.Run();
            }
        }

        if(firstHasRun)
        {
            var afterTime = timer - firstTime;
            for (int i = 0; i < enemyPoints.Length; i++)
            {
                var periods = period * i;
                if (afterTime >= periods)
                {
                    if (enemyPoints[i].IsApp == false)
                    enemyPoints[i].EnemyInstance();
                   // Debug.Log(i + "  ,Instance");
                }
            }
        }
    }

    private Shooting.EnemyPoint[] PointsShuffle(Shooting.EnemyPoint[] array)
    {
        int length = array.Length;
        Shooting.EnemyPoint[] result = new Shooting.EnemyPoint[length];
        array.CopyTo(result, 0);

        for (int i = 0; i < length; i++)
        {
            Shooting.EnemyPoint tmp = result[i];
            int randomIndex = Random.Range(i, length);
            result[i] = result[randomIndex];
            result[randomIndex] = tmp;
        }

        return result;

    }
}
