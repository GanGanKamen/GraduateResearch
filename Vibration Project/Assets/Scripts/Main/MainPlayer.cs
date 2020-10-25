using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [Header("Defult")]
    [SerializeField] private Transform front;
    [SerializeField] private Camera mainCamera;
    [Header("Paramater")]
    [SerializeField] private float hp;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int hitPointEndruance;
    [SerializeField] private float bloodRecoveryTime;
    [SerializeField] private int armorShield;
    [Header("Manager")]
    [SerializeField] private HitManager hitManager;
    [SerializeField] private PostManager postManager;

    public Vector3 FrontVec { get { return CharacterVec(); } }
    public bool Dead { get { return _dead; } }


    private HitPoint[] hitPoints;
    private bool _dead = false;
    // Start is called before the first frame update
    void Start()
    {
        HitPointsInit();
        hitManager.Init(this);
        postManager.Init(hp);
    }

    // Update is called once per frame
    void Update()
    {
        hitManager.HitMarkUpdate();
        postManager.PostUpdate();
        KeyCtrl();
    }

    public void GetDamege(Transform target)
    {
        if (_dead) return;
        if(armorShield > 0)
        {
            armorShield -= 1;
            hitManager.GetDamege(target, HP_Status.Normal);
            if (armorShield <= 0) armorShield = 0;
        }
        else
        {
            if (hp > 0) hp -= 1;
            hitManager.GetDamege(target, HP_Status.Pinch);
            if (postManager.IsPost == false) postManager.PostON();
            postManager.GetPostAction(PostManager.Action.Down);
            if (hp <= 0)
            {
                hp = 0;
                _dead = true;
            }
        }
    }

    private void HitPointsInit()
    {
        var hitpointsList = new List<HitPoint>();
        for (int i = 0; i < 4; i++)
        {
            var hitPoint = new HitPoint(hitPointEndruance, bloodRecoveryTime);
            hitpointsList.Add(hitPoint);
        }
        hitPoints = hitpointsList.ToArray();
    }

    private Vector3 CharacterVec()
    {
        var pos0 = transform.position;
        var pos1 = front.position;
        return (pos1 - pos0).normalized;
    }

    private void KeyCtrl()
    {
        if (_dead) return;
        if (Input.GetAxis("Horizontal") != 0)
            transform.localEulerAngles += 
                new Vector3(0, rotateSpeed * Time.deltaTime* Input.GetAxis("Horizontal"), 0);

    }

    

    private int GetHitPointNumber(float angle)
    {
        float angleDelta = 360f / 4f;
        float startAngle = angleDelta / 2f;
        if (angle >= startAngle && angle < 360f - startAngle)
        {
            return (int)Mathf.Ceil((angle - startAngle) / angleDelta);
        }

        else return 0;
    }
}
