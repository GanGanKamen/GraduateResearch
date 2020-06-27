using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLook : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rot = Quaternion.LookRotation(target.position);
        var angle = rot.eulerAngles + new Vector3(-80f, 0,0);
        weapon.eulerAngles = angle;
        //weapon.LookAt(target.position);
    }
}
