using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{

    private CharacterAnimations playerAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        playerAnimation = GetComponent<CharacterAnimations>();
        

    }



    // Update is called once per frame
    void Update()
    {
        //Attack 
        if(Input.GetKey(KeyCode.K))
        {
            playerAnimation.Attack();
          
            //if you are using two different attacks
          //  if(Random.Range(0, 2) > 0) {

             //   playerAnimation.Attack();

          //  }
            

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimation.Jump();

        }


    }



















} //class
