using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    private bool isInited = false;

    private MainPlayer player;
    private DamageMark[] marks;
    [SerializeField] private float activeTime;

    private int nowActiveSize = 0;
    private int maxActiveSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInited)
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].MarkRotate();
        }
    }

    public void Init(MainPlayer mainPlayer)
    {
        player = mainPlayer;
        var markList = new List<DamageMark>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<DamageMark>() != null)
            {
                var mark = transform.GetChild(i).GetComponent<DamageMark>();
                markList.Add(mark);
            }
        }
        marks = markList.ToArray();

        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].Init(player.transform, activeTime, this);
        }

        maxActiveSize = marks.Length;

        isInited = true;
    }

    public void GetDamege(Transform target, HP_Status hP_Status)
    {
        marks[nowActiveSize].SetMarkActive(target, hP_Status);
    }

    public void UseMark()
    {
        if(nowActiveSize < maxActiveSize)
        nowActiveSize += 1;
    }

    public void ReleaseMark()
    {
        if(nowActiveSize > 0)
        nowActiveSize -= 1;
    }
}
