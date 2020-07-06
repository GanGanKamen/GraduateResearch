using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class HitPoint
    {
        public string name { get; set; }
        public bool isHeat { get; set; }
        public Transform transform { get; set; }
        public float distance { get; set; }

        public HitPoint(string _name, Transform _transform)
        {
            name = _name;
            transform = _transform;
        }
    }

    public class HitManager : MonoBehaviour
    {
        [SerializeField] private bool vestSystem;
        [SerializeField] private GameObject hitPrefab;
        [SerializeField] private GameObject bloodPrefab;
        private HitPoint[] hitPoints;

        // Start is called before the first frame update
        void Start()
        {
            var list = new List<HitPoint>();
            foreach(Transform child in transform)
            {
                var hitPoint = new HitPoint(child.name, child.transform);
                list.Add(hitPoint);
            }

            hitPoints = list.ToArray();
        }

        public void GetDamage(Vector3 muzzle,bool danger)
        {
            var sortList = new List<HitPoint>();

            for(int i = 0; i < hitPoints.Length; i++)
            {
                var dis = Vector3.Distance(muzzle, hitPoints[i].transform.position);
                hitPoints[i].distance = dis;
                sortList.Add(hitPoints[i]);
            }

            sortList.Sort((a, b) => (int)a.distance - (int)b.distance);
            Debug.Log(sortList[0].name);
            var hitObj = Instantiate(hitPrefab, sortList[0].transform.position, Quaternion.LookRotation(muzzle));
            if (danger)
            {
                var bloodObj = Instantiate(bloodPrefab, sortList[0].transform.position, Quaternion.LookRotation(muzzle));
            }
        }
    }
}

