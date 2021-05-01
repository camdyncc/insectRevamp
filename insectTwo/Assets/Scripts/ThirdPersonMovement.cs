using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;

    public float gravity = 20f;
    public float jump_Force = 7;
    private float vertical_Velocity;
    public BoxCollider col;
    private Animator anim;


    private Rigidbody rb;
     public LayerMask ground;
      
      
     
     
     
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
       

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Animations();


        if(direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;

            controller.Move(moveDir * speed * Time.deltaTime);
        }

        jump();
   


    }

    private void jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jump_Force, ForceMode.Impulse);
           // vertical_Velocity = jump_Force;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), ground);

        
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

    /* void ApplyGravity()
     {
         vertical_Velocity -= gravity * Time.deltaTime;

         //jump
         PlayerJump();

         //Direction.z = vertical_Velocity * Time.deltaTime;

     }
     void PlayerJump()
     {

         if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
         {
             vertical_Velocity = jump_Force;
         }

     }*/

}
