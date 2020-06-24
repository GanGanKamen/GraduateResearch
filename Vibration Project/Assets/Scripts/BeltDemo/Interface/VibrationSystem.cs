using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerialPortUtility;

namespace Interface
{
    public class VibrationSystem : MonoBehaviour
    {
        [SerializeField] private VibrationEffect effect;
        [SerializeField][Range(0,1)] private float vibrationPower;
        [SerializeField] private float vibrationTime;
        [SerializeField] private VibrationUnitType unitType;
        //public SerialHandler serialHandler;
        public SerialPortUtilityPro serialHandler;
        private VibrationUnit[] vibrationUnits;

        [SerializeField]private bool[] isVibrates = new bool[4];
        [SerializeField]private float[] vibrateCounts = new float[4];
        //private List<IEnumerator> nowVibrateActions = new List<IEnumerator>();
        private IEnumerator nowVibrateAction = null;
        void Start()
        {
            serialHandler = GetComponent<SerialPortUtilityPro>();
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
                var vibrationUnit = new VibrationUnit(this,direction,unitType);
                vibrationUnitList.Add(vibrationUnit);
                vibrateCounts[i] = 0;
                isVibrates[i] = false;
            }
            vibrationUnits = vibrationUnitList.ToArray();            
        }

        void Update()
        {
            StopTimeVibration();
        }

        public void TestOutput(float angle)
        {
            Debug.Log(AngleToDirectionNumber(angle));
        }

        public void PlayVibration(float angle)
        {
            var dirNum = AngleToDirectionNumber(angle);
            //Debug.Log("dirNum:" + AngleToDirectionNumber(angle));
            switch (unitType)
            {
                case VibrationUnitType.Proto:                   
                    if (dirNum >= 7)
                    {
                        vibrationUnits[0].PlayVibrationOneShot(vibrationPower, vibrationTime);
                        vibrationUnits[3].PlayVibrationOneShot(vibrationPower, vibrationTime);
                    }
                    else if (dirNum % 2 == 0)
                    {
                        vibrationUnits[dirNum / 2].PlayVibrationOneShot(vibrationPower, vibrationTime);
                    }
                    else
                    {
                        vibrationUnits[(dirNum + 1) / 2].PlayVibrationOneShot(vibrationPower, vibrationTime);
                        vibrationUnits[(dirNum - 1) / 2].PlayVibrationOneShot(vibrationPower, vibrationTime);
                    }
                    break;
                case VibrationUnitType.DX:

                    //StopNowAction();
                    /*
                    if (dirNum >= 7)
                    {
                        var vibrateAction0 = OneShotCoroutine(0, 3, vibrationPower, vibrationTime);
                        nowVibrateAction = vibrateAction0;
                        StartCoroutine(nowVibrateAction);
                    }
                    else if (dirNum % 2 == 0)
                    {
                        var unitNum = dirNum / 2;
                        var vibrateAction0 = OneShotCoroutine(unitNum, vibrationPower, vibrationTime);
                        nowVibrateAction= vibrateAction0;
                        StartCoroutine(nowVibrateAction);
                    }
                    else
                    {
                        var unitNum0 = (dirNum + 1) / 2;
                        var unitNum1 = (dirNum - 1) / 2;
                        var vibrateAction0 = OneShotCoroutine(unitNum0,unitNum1, vibrationPower, vibrationTime);
                        nowVibrateAction = vibrateAction0;
                        StartCoroutine(nowVibrateAction);
                    }*/
                    if (dirNum >= 7)
                    {
                        Shot(0, vibrationPower);
                        Shot(3, vibrationPower);
                    }
                    else if (dirNum % 2 == 0)
                    {
                        var unitNum = dirNum / 2;
                        Shot(unitNum, vibrationPower);

                    }
                    else
                    {
                        var unitNum0 = (dirNum + 1) / 2;
                        var unitNum1 = (dirNum - 1) / 2;
                        Shot(unitNum0, vibrationPower);
                        Shot(unitNum1, vibrationPower);
                    }
                    
                    break;
            }
            
        }

        private void Shot(int num,float Power)
        {
            if (num > 3) return;
            if (isVibrates[num]) return;
            string serialMessage = UnitNumberToSerialMessage(num);
            serialHandler.Write(serialMessage);
            isVibrates[num] = true;
        }

        private void StopTimeVibration()
        {
            for(int i = 0; i < 4; i++)
            {
                if (isVibrates[i])
                {
                    if(vibrateCounts[i] >= vibrationTime)
                    {
                        vibrateCounts[i] = 0;
                        string serialStop = UnitNumberToSerialMessage(i);
                        serialHandler.Write(serialStop);
                        isVibrates[i] = false;

                    }
                    else
                    {
                        vibrateCounts[i] += Time.deltaTime;
                    }
                }
            }
        }

        private void StopNowAction()
        {
            //if (nowVibrateAction == null) return;
            //StopCoroutine(nowVibrateAction);
            
            serialHandler.Write("a0;");
            serialHandler.Write("b0;");
            serialHandler.Write("c0;");
            serialHandler.Write("d0;");
            
            for(int i = 0;i< 4; i++)
            {
                vibrateCounts[i] = 0;
                isVibrates[i] = false;
            }
            //nowVibrateAction = null;
        }

        private IEnumerator OneShotCoroutine(int num, float Power, float time)
        {
            if (num > 3) yield break;
            string serialMessage = UnitNumberToSerialMessage(num) + Power.ToString() + ";";
            string serialStop = UnitNumberToSerialMessage(num) + "0;";
            serialHandler.Write(serialMessage);
            Debug.Log(serialMessage);
            yield return new WaitForSeconds(time);
            serialHandler.Write(serialStop);
            yield break;
        }

        private IEnumerator OneShotCoroutine(int num0, int num1, float Power, float time)
        {
            if (num0 > 3||num1 > 3) yield break;
            string serialMessage0 = UnitNumberToSerialMessage(num0) + Power.ToString() + ";";
            string serialStop0 = UnitNumberToSerialMessage(num0) + "0;";
            string serialMessage1 = UnitNumberToSerialMessage(num1) + Power.ToString() + ";";
            string serialStop1 = UnitNumberToSerialMessage(num1) + "0;";
            serialHandler.Write(serialMessage0);
            serialHandler.Write(serialMessage1);
            Debug.Log(serialMessage0 + serialMessage1);
            yield return new WaitForSeconds(time);
            serialHandler.Write(serialStop0);
            serialHandler.Write(serialStop1);
            yield break;
        }

        private string UnitNumberToSerialMessage(int num)
        {
            switch (num)
            {
                case 0:
                    if (isVibrates[0]) return "h";
                    else return "a";
                case 1:
                    if (isVibrates[1]) return "i";
                    else return "b";
                case 2:
                    if (isVibrates[2]) return "j";
                    else return "c";
                case 3:
                    if (isVibrates[3]) return "k";
                    else return "d";
                default:
                    return "";
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

