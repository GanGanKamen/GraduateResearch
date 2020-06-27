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
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
        /*
        Vector3 rightPos = targetR.position;
        rightPos.x = animator.GetIKPosition(AvatarIKGoal.RightHand).x;
        rightPos.z = animator.GetIKPosition(AvatarIKGoal.RightHand).z;
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightPos);
        
        Vector3 leftPos = targetL.position;
        leftPos.x = animator.GetIKPosition(AvatarIKGoal.LeftHand).x;
        leftPos.z = animator.GetIKPosition(AvatarIKGoal.LeftHand).z;
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftPos);

        //animator.SetLookAtWeight(ikWeight);
        //animator.SetLookAtPosition(target.position);
        */
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetR.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, targetL.position);

        //animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

        animator.SetIKRotation(AvatarIKGoal.RightHand, targetR.rotation);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, targetL.rotation);
    }
}
