using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIwase : MonoBehaviour
{
    [SerializeField] private Transform foward;
    [SerializeField] private Camera mainCamera;

    private bool canStart = false;
    private bool isAction = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFront();
    }

    private void GotoNext(string name)
    {
        if (isAction == false)
        {
            Fader.FadeIn(2f, name, true);
            isAction = true;
        }

    }

    private void CheckFront()
    {
        var vec = Vector3.Scale((foward.position - transform.position)
            , new Vector3(1, 0, 1)).normalized;
        var cameraVec = Vector3.Scale(mainCamera.transform.forward, 
            new Vector3(1, 0, 1));

        var dot = Vector3.Dot(vec, cameraVec);
        Debug.Log(dot); // dot<-0.9f
    }
}
