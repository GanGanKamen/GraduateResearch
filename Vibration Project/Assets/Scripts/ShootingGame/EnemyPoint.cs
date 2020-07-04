using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class EnemyPoint : MonoBehaviour
    {
        public Vector3 FirstPosition { get { return transform.position; } }
        public Vector3 SecondPosition { get { return second.position; } }
        [SerializeField] private Transform second;
    }
}

