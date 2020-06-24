using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class SoldierBase : MonoBehaviour
    {
        public GameObject Body { get { return body; } }
        [SerializeField] private float runSpeed;
        [SerializeField] private GameObject body;
        [SerializeField] private Animator soldierAnimator;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void CharacterMove(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.Translate(direction * Time.deltaTime * runSpeed);
            body.transform.localRotation = Quaternion.LookRotation(direction);
        }
    }
}

