﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class BaseInfo : MonoBehaviour
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

        public BaseInfo(string _name, SexGroup _sex,int _playerSkill)
        {
            playerName = _name;
            sex = _sex;
            if (_playerSkill <= 0) playerSkill = 0;
            else if (_playerSkill >= 3) playerSkill = 3;
            else playerSkill = _playerSkill;
        }
    }
}
