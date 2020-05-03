using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationSystem : MonoBehaviour
    {
        [SerializeField] private VibrationEffect effect;
        [SerializeField][Range(0,1)] private float vibrationPower;
        [SerializeField] private float vibrationTime;
        [SerializeField] private VibrationUnitType unitType;
        private VibrationUnit[] vibrationUnits;

        private void Awake()
        {
            List<VibrationUnit> vibrationUnitList = new List<VibrationUnit>();
            for (int i = 0; i < 4; i++)
            {
                var direction = UnitCategoly.Foward;
                switch (i)
                {
                    default:
                        break;
                    case 1:
                        direction = UnitCategoly.Left;
                        break;
                    case 2:
                        direction = UnitCategoly.Back;
                        break;
                    case 3:
                        direction = UnitCategoly.Right;
                        break;
                }
                var vibrationUnit = new VibrationUnit(direction,unitType);
                vibrationUnitList.Add(vibrationUnit);
            }
            vibrationUnits = vibrationUnitList.ToArray();
        }

        void Update()
        {
            //テスト用
            /*
            if (Input.GetKeyDown(KeyCode.A))
            {
                XInputDotNetPure.GamePad.SetVibration(0, 0.2f, 0.2f);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                XInputDotNetPure.GamePad.SetVibration(0, 3f, 3f);
            }
            */
        }

        public void TestOutput(float angle)
        {
            Debug.Log(AngleToDirectionNumber(angle));
        }

        public void PlayVibration(float angle)
        {
            var dirNum = AngleToDirectionNumber(angle);
            /*
            switch (dirNum)
            {
                default:
                    vibrationUnits[0].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
                case 1:
                    vibrationUnits[0].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    vibrationUnits[1].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
                case 2:
                    vibrationUnits[1].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
                case 3:
                    vibrationUnits[1].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    vibrationUnits[2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
                case 4:
                    vibrationUnits[2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
                case 5:
                    vibrationUnits[2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    vibrationUnits[3].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                    break;
            }
            */
            if(dirNum >= 7)
            {
                vibrationUnits[0].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                vibrationUnits[3].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
            }
            else if(dirNum % 2 == 0)
            {
                vibrationUnits[dirNum / 2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
            }
            else
            {
                vibrationUnits[(dirNum + 1) / 2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
                vibrationUnits[(dirNum - 1) / 2].PlayVibrationOneShot(this, vibrationPower, vibrationPower, vibrationTime);
            }
        }

        private int AngleToDirectionNumber(float angle)
        {
            float angleDelta = 360f / 8f;
            float startAngle = 0 + angleDelta / 2f;
            if (angle > startAngle && angle <= 360f - startAngle)
            {
                return (int)Mathf.Ceil((angle - startAngle) / angleDelta);
            }
            else
            {
                return 0;
            }
        }

    }
}

