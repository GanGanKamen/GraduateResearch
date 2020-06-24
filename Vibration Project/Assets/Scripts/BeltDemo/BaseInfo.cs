using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class BaseInfo
    {
        public enum SexGroup
        {
            Man,
            Woman,
            Other
        }

        public string playerName;
        public SexGroup sex;
        public int playerSkill;
        public int age;

        public BaseInfo(string _name, SexGroup _sex,int _age,int _playerSkill)
        {
            playerName = _name;
            sex = _sex;
            age = _age;
            if (_playerSkill <= 0) playerSkill = 0;
            else if (_playerSkill >= 3) playerSkill = 3;
            else playerSkill = _playerSkill;
        }
    }
}

