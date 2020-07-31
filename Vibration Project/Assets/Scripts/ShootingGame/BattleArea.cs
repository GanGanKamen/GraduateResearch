using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class BattleArea : MonoBehaviour
    {
        [SerializeField] private StageManager stageManager;
        private bool isCollide = false;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isCollide == false)
            {
                isCollide = true;
                stageManager.FirstInstance();
            }
        }

    }
}

