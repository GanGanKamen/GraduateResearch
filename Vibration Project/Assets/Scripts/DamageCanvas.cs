using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageCanvas : MonoBehaviour
    {
        [HideInInspector] public Transform targetEnemy;
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
            targetEnemy = _target;
            mark.SetActive(true);
            yield return new WaitForSeconds(activeTime);
            mark.SetActive(false);
            //targetEnemy = null;
            isDamaged = false;
            yield break;
        }

        private void MarkRotate()
        {
            if (targetEnemy != null)
            {
                center.localEulerAngles = new Vector3(0, 0, CenterAngle());
            }
        }

        private float CenterAngle()
        {
            var targetPos = new Vector2(targetEnemy.position.x, targetEnemy.position.z);
            var playerPos = new Vector2(player.position.x, player.position.z);
            float dx = targetPos.x - playerPos.x;
            float dy = targetPos.y - playerPos.y;
            float rad = Mathf.Atan2(dy, dx);
            return rad * Mathf.Rad2Deg + player.eulerAngles.y - 90f;
        }
    }
}

