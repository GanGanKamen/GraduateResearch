using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shooting
{
    public class CameraMark : MonoBehaviour
    {
        [SerializeField] private Transform lookat;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(lookat);
        }
    }
}

