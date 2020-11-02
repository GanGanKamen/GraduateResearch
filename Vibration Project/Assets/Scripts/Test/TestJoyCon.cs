using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoyCon : MonoBehaviour
{
    private List<Joycon> joycons;
    [SerializeField] private Transform target0;
    [SerializeField] private Transform target1;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        joycons = JoyconManager.Instance.j;
    }

    // Update is called once per frame
    void Update()
    {
        target0.transform.eulerAngles -= new Vector3(0,joycons[0].GetGyro().x*Time.deltaTime * speed, 0);
        target1.transform.eulerAngles -= new Vector3(0, joycons[1].GetGyro().x* Time.deltaTime * speed, 0);
    }
}
