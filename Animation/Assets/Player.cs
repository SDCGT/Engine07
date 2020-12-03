using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private int speedID = Animator.StringToHash("Speed");
    private int isSpeedupID = Animator.StringToHash("IsSpeedup");
    private int horizontalID = Animator.StringToHash("Horizontal");

    private int speedRotateID = Animator.StringToHash("SpeedRotate");
    private int speedZID = Animator.StringToHash("SpeedZ");

    private int vaultID = Animator.StringToHash("Vault");
    public Vector3 matchTarget = Vector3.zero;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(speedZID, Input.GetAxis("Vertical") * 4.1f);
        anim.SetFloat(speedRotateID, Input.GetAxis("Horizontal") * 126);
        bool isVault = false;
        if(anim.GetFloat(speedZID)>3&&anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position+Vector3.up*0.3f,transform.forward,out hit,4f))
            {
                if(hit.collider.tag=="Obstacle")
                {
                    if(hit.distance>3)
                    {
                        Vector3 point = hit.point;
                        point.y = hit.collider.transform.position.y + hit.collider.bounds.size.y+0.07f;
                        matchTarget = point;
                        isVault = true;
                    }
                }
            }
        }
        anim.SetBool(vaultID, isVault);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Vault")&&anim.IsInTransition(0)==false)
        {
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one,0), 0.32f, 0.4f);
        }
    }
}
