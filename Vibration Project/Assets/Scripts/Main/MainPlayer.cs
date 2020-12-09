using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MainPlayer : MonoBehaviour
{
    public Transform Body { get { return _body; } }
    [Header("Defult")]
    [SerializeField] private Transform front;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform _body;
    [Header("Paramater")]
    [SerializeField] private float hp;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int hitPointEndruance;
    [SerializeField] private float bloodRecoveryTime;
    [SerializeField] private int armorShield;
    
    //[Header("Manager")]
    //[SerializeField] private HitManager hitManager;
   // [SerializeField] private PostManager postManager;

    public Vector2 FrontVec2D { get { return Vec2D(); } }
    public Vector3 FrontVec3D { get { return Vec3D(); } }
    public bool Dead { get { return _dead; } }

    private HitPoint[] hitPoints;
    private bool _dead = false;

    private bool _isInit = false;
    private Primitive.StageManager _stageManager;
    public void Init(Primitive.StageManager stageManager)
    {
        HitPointsInit();
        _stageManager = stageManager;
        Body.localEulerAngles = Vector3.zero;
        _isInit = true;

    }

    public void GetDamege(Transform target)
    {
        if (_dead) return;
        if(armorShield > 0)
        {
            armorShield -= 1;
            if (armorShield <= 0) armorShield = 0;
        }
        else
        {
            if (hp > 0) hp -= 1;
            _stageManager.PlayGetDamage(target);
            if (hp <= 0 && _dead == false)
            {
                hp = 0;
                Die();
            }
        }
    }


    public void KeyCtrlTest()
    {
        if (_dead || _isInit == false) return;
        if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
            transform.localEulerAngles +=
                new Vector3(rotateSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 
                rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0);
    }

    public void BodyRotate(float input)
    {
        if (_dead || _isInit == false) return;
        _body.localEulerAngles -= new Vector3(0, rotateSpeed * Time.deltaTime * input, 0);
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

    private Vector3 Vec3D()
    {
        var pos0 = transform.position;
        var pos1 = front.position;
        return (pos1 - pos0).normalized;
    }

    private Vector2 Vec2D()
    {
        var pos0 = new Vector2(transform.position.x,transform.position.z);
        var pos1 = new Vector2(front.position.x, front.position.z);
        return (pos1 - pos0).normalized;
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

    private void Die()
    {
        _dead = true;
        hp = 0;
        _stageManager.PlayerDie();
    }
}
