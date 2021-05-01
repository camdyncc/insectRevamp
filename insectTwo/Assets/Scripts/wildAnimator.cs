using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildAnimator : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        Animations();
    }


    public void Animations()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
            anim.Play("walk_ani_vor");

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    anim.Play("run_ani_vor");
                }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("Jump");
        }


       
    }

}
