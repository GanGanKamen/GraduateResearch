using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooting
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Slider hpBar;

        [SerializeField] private Text debugText;

        private bool isRefresh = false;
        private float refreshCount = 0;
        // Start is called before the first frame update
        void Start()
        {
            hpBar.maxValue = player.MaxHp;
        }

        // Update is called once per frame
        void Update()
        {
            hpBar.value = player.HP;

            if (isRefresh)
            {
                if(refreshCount < 1)
                {
                    refreshCount += Time.deltaTime;
                }
                else
                {
                    refreshCount = 0;
                    isRefresh = false;
                }
            }
        }

        public void TextOutPut(string msg)
        {
            debugText.text = msg;
            if (isRefresh)
            {
                refreshCount = 0;
            }
            else
            {
                isRefresh = true;
            }
        }
    }
}

