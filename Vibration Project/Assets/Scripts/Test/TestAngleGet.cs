﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngleGet : MonoBehaviour
{
    [SerializeField] float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = transform.eulerAngles.y;
    }
}
