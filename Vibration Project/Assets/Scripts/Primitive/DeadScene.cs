using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScene : MonoBehaviour
{
    private bool isInput = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isInput == false)
        {
            isInput = true;
            Fader.FadeInBlack(1f,"Start",true);
        }
    }
}
