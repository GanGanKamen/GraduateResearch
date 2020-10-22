using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HP_Status
{
    Normal,
    Pinch
}

public class DamageMark : MonoBehaviour
{
    public Transform Target { get { return _target; } }
    public bool Active { get { return active; } }
    [SerializeField] private RectTransform center;
    [SerializeField] private Image mark;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color bloodColor;
    private Transform _player;
    private bool active = false;
    private Transform _target;
    private float _activeTime;
    private HitManager manager;
    public void Init(Transform player,float activeTime,HitManager hitManager)
    {
        _player = player;
        _activeTime = activeTime;
        manager = hitManager;
    }

    public void SetMarkActive(Transform target,HP_Status hP_Status)
    {
        StartCoroutine(MarkActiveCoroutine(target, hP_Status));
    }

    public void MarkRotate() //Update()
    {
        if (_target != null)
        {
            center.localEulerAngles = new Vector3(0, 0, CenterAngle());
        }
    }

    public float GetEulerAngle(Transform target)
    {
        _target = target;
        center.localEulerAngles = new Vector3(0, 0, CenterAngle());
        return center.localEulerAngles.z;
    }

    private IEnumerator MarkActiveCoroutine(Transform target, HP_Status hP_Status)
    {
        if (active) yield break;
        manager.UseMark();
        active = true;
        var target0 = new GameObject();
        target0.transform.position = target.position;
        _target = target0.transform;
        center.localEulerAngles = new Vector3(0, 0, CenterAngle());
        switch (hP_Status)
        {
            case HP_Status.Normal:
                mark.color = normalColor;
                break;
            case HP_Status.Pinch:
                mark.color = bloodColor;
                break;
        }
        mark.gameObject.SetActive(true);
        yield return new WaitForSeconds(_activeTime);
        mark.gameObject.SetActive(false);
        active = false;
        _target = null;
        manager.ReleaseMark();
        Destroy(target0);
        yield break;
    }


    private float CenterAngle()
    {
        var targetPos = new Vector2(_target.position.x, _target.position.z);
        var playerPos = new Vector2(_player.position.x, _player.position.z);
        float dx = targetPos.x - playerPos.x;
        float dy = targetPos.y - playerPos.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg + _player.eulerAngles.y - 90f;
    }

}
