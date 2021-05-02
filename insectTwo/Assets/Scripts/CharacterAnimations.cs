using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator anim;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

   public void Idle(bool idle)
    {
        anim.SetBool(AnimationTags.IDLE_PARAMETER, idle);
    }

    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }

    public void Run(bool run)
    {
        anim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }

    public void Jump()
    {
        anim.SetTrigger(AnimationTags.JUMP_PARAMETER);

    }

    public void Attack()
    {
        anim.SetTrigger(AnimationTags.ATTACK_PARAMETER);
    }


   

}
