using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageCanvas : MonoBehaviour
    {
        public float MarkAngle { get { return center.localEulerAngles.z; } }
        [HideInInspector] public Transform target;
        public bool IsDamaged { get { return isDamaged; } }
        private Transform player;
        [SerializeField] private Transform target0;
        [SerializeField] private RectTransform center;
        [SerializeField] private GameObject mark;
        [SerializeField] private float activeTime;

        private bool isDamaged = false;

        void Start()
        {
            mark.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            MarkRotate();
        }

        public void SetMarkActive(Transform _target)
        {
            StartCoroutine(MarkActiveCoroutine(_target));
        }

        private IEnumerator MarkActiveCoroutine(Transform _target)
        {
            if (isDamaged) yield break;
            isDamaged = true;
            var target0 = new GameObject();
            target0.transform.position = _target.position;
            target = target0.transform;
            mark.SetActive(true);
            yield return new WaitForSeconds(activeTime);
            mark.SetActive(false);
            //targetEnemy = null;
            isDamaged = false;
            Destroy(target0);
            yield break;
        }

        private void MarkRotate()
        {
            if (target != null)
            {
                center.localEulerAngles = new Vector3(0, 0, CenterAngle());
            }
            //Debug.Log(center.localEulerAngles.z); //Range 0 ~ 360 反時計回り
        }

        private float CenterAngle()
        {
            var targetPos = new Vector2(target.position.x, target.position.z);
            var playerPos = new Vector2(player.position.x, player.position.z);
            float dx = targetPos.x - playerPos.x;
            float dy = targetPos.y - playerPos.y;
            float rad = Mathf.Atan2(dy, dx);
            return rad * Mathf.Rad2Deg + player.eulerAngles.y - 90f; //angleRange:-180 ~ 180
        }
    }
}

