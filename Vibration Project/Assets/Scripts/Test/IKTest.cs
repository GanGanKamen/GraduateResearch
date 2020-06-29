using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isIK;
    [SerializeField] private Transform targetL;
    [SerializeField] private Transform targetR;
    [SerializeField] private GameObject weapon;
    private float ikWeight = 0;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(weapon.transform.localEulerAngles.y);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isIK)
        {
            Debug.Log("ik");
            if (ikWeight < 1.0f)
            {
                ikWeight += Time.deltaTime;
                if (ikWeight > 1.0f)
                {
                    ikWeight = 1.0f;
                }
            }
            
        }
        else
        {
            if (ikWeight > 0.0f)
            {
                ikWeight -= Time.deltaTime;
                if (ikWeight < 0.0f)
                {
                    ikWeight = 0.0f;
                }
            }
        }
        animator.SetLookAtWeight(ikWeight, ikWeight, ikWeight, ikWeight, ikWeight);
        var targetVec = (target.position - weapon.transform.position) * 100;
        Debug.DrawLine(weapon.transform.position, target.position, Color.blue);
        animator.SetLookAtPosition(target.position);
    }
}
