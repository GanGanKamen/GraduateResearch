using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoInputCanvas : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private InputField name_inputField;
        [SerializeField] private Text worning_text;
        [SerializeField] private Toggle[] sex_group;
        [SerializeField] private Toggle[] skill_group;
        [SerializeField] private Button next_button;
        // Start is called before the first frame update
        void Start()
        {
            next_button.onClick.AddListener(() => GoToNext());
        }

        public void GoToNext()
        {
            if (name_inputField.text == "")
            {
                worning_text.gameObject.SetActive(true);
                return;
            }

            var playerName = name_inputField.text;

            var sex = player.BaseInfo.SexGroup.Man;
            if (sex_group[0].isOn) sex = player.BaseInfo.SexGroup.Man;
            else if (sex_group[1].isOn) sex = player.BaseInfo.SexGroup.Woman;
            else sex = player.BaseInfo.SexGroup.Other;

            var playerSkill = 0;
            for(int i = 0; i < skill_group.Length; i++)
            {
                if (skill_group[i].isOn)
                {
                    playerSkill = i;
                    break;
                }
            }

            var info = new player.BaseInfo(playerName, sex, playerSkill);
            System.GameSystem.baseInfo = info;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}

