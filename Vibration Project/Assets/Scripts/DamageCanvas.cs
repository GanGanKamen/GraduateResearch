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
        [SerializeField] private Transform target0;
        [SerializeField] private Transform player;
        [SerializeField] private RectTransform center;
        [SerializeField] private GameObject mark;
        [SerializeField] private float activeTime;

        private bool isDamaged = false;

        void Start()
        {
            mark.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetMarkActive(target0);
            }
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
            Debug.Log("active");
            yield return new WaitForSeconds(activeTime);
            mark.SetActive(false);
            targetEnemy = null;
            isDamaged = false;
            Debug.Log("activeFalse");
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
            return rad * Mathf.Rad2Deg - 90f;
        }
    }
}

