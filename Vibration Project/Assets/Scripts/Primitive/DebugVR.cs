using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugVR : MonoBehaviour
{
    [SerializeField] private RectTransform bodyMark;
    [SerializeField] private Text gyiroInput;
    [SerializeField] private Text serial;
    [SerializeField] private GameObject[] bloodMarks;
    [SerializeField] private GameObject[] hitMarks;
    private MainPlayer player;
    private VestManager vestManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        vestManager = GameObject.Find("VestManager").GetComponent<VestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        BodyRotateUpdate();
        GyiroTextUpdate();
        PartsUpdate();
    }

    private void BodyRotateUpdate()
    {
        bodyMark.localEulerAngles = new Vector3(0, 0,360- player.Body.localEulerAngles.y);
    }

    private void GyiroTextUpdate()
    {
        serial.text = "Serial:   " + vestManager.Paramater.ToString();
        gyiroInput.text = "Input:  " + vestManager.GyiroInput.ToString(); 
    }

    private void PartsUpdate()
    {
        for(int i = 0; i < 4; i++)
        {
            hitMarks[i].SetActive(vestManager.DebugHitParts[i].IsVibrate);
            bloodMarks[i].SetActive(vestManager.DebugHitParts[i].IsHeat);
        }
    }
}
