using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVec : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform front;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var vec0 = (new Vector2(front.position.x, front.position.z) -
            new Vector2(player.position.x, player.position.z)).normalized;
        var vec1 = (new Vector2(target.position.x, target.position.z) -
            new Vector2(player.position.x, player.position.z)).normalized;
        var cross = vec0.x * vec1.y - vec0.y * vec1.x;
        Debug.Log(cross);
        */
        Debug.DrawRay(transform.position, transform.forward, Color.red,5);
    }
}
