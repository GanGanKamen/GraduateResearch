using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private HitManager hitManager;

    // Start is called before the first frame update
    void Start()
    {
        hitManager.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
