using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace UI
{
    public class InfoInputCanvas : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        [SerializeField] private InputField name_inputField;
        [SerializeField] private InputField age_inputField;
        [SerializeField] private Text[] worning_text;
        [SerializeField] private Toggle[] sex_group;
        [SerializeField] private Toggle[] skill_group;
        [SerializeField] private Button next_button;
        // Start is called before the first frame update
        void Start()
        {
            next_button.onClick.AddListener(() => GoToNext());
        }

        private void Update()
        {

        }

        public void GoToNext()
        {
            if (name_inputField.text == "")
            {
                worning_text[0].gameObject.SetActive(true);
            }
            if(age_inputField.text == "")
            {
                worning_text[1].gameObject.SetActive(true);
            }

            if(name_inputField.text == "" || age_inputField.text == "")
            {
                return;
            }
            var PlayerName = name_inputField.text;

            var sex = Player.BaseInfo.SexGroup.Man;
            if (sex_group[0].isOn) sex = Player.BaseInfo.SexGroup.Man;
            else if (sex_group[1].isOn) sex = Player.BaseInfo.SexGroup.Woman;
            else sex = Player.BaseInfo.SexGroup.Other;

            var PlayerSkill = 0;
            for(int i = 0; i < skill_group.Length; i++)
            {
                if (skill_group[i].isOn)
                {
                    PlayerSkill = i;
                    break;
                }
            }
            var ageNum = int.Parse(age_inputField.text);
            var info = new Player.BaseInfo(PlayerName, sex, ageNum, PlayerSkill);
            System.GameSystem.baseInfo = info;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }

        public void TextSave(string msg)
        {
            var PlayerName = name_inputField.text;
            string txt = msg + "\n" + msg;
            FileInfo fileInfo = new FileInfo(Application.dataPath + "/" +PlayerName + "Log.txt");
            StreamWriter writer = fileInfo.AppendText();
            writer.WriteLine(txt);
            writer.Flush();
            writer.Close();
        }
    }
}

