using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public class VibrationSystem : MonoBehaviour
    {
        private VibrationUnit[] vibrationUnits;

        private void Awake()
        {
            List<VibrationUnit> vibrationUnitList = new List<VibrationUnit>();
            for(int i =0; i < 4; i++)
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
                var vibrationUnit = new VibrationUnit(direction);
                vibrationUnitList.Add(vibrationUnit);
            }
            vibrationUnits = vibrationUnitList.ToArray();
        }

        void Start()
        {
            //Debug.Log(vibrationUnits[1].VibrationUnitCategoly);
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                vibrationUnits[1].PlayVibrationOneShot(1, 1, 100);
            }
        }

        private void HitPointToUnitDirectin(Vector3 hitPoint,Vector3 damagedCharacter)
        {
            var hitPoint0 = new Vector3(hitPoint.x, 0, hitPoint.z);
            var damagedCharacter0 = new Vector3(damagedCharacter.x, 0, damagedCharacter.z);
        }
    }
}

