using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
public class TestVR : MonoBehaviour
{
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private float hight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        left.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand) + new Vector3(0,hight,0);
        right.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(0, hight, 0);
    }
}
