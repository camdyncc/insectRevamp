using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{


    private CharacterController charController;
    private CharacterAnimations playerAnimations;

    public float movement_Speed = 3f;
    public float gravity = 9.8f;
    public float roation_Speed = 0.15f;
    public float rotateDegreesPerSecond = 180f;

    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<CharacterAnimations>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);



    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        AnimateWalk();
        Jump();
        
       
    }

    void Move()
    {
        if (Input.GetAxis(Axis.VERTICAL_AXIS) > 0)
        {

            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_Speed * Time.deltaTime);

        }
        else if (Input.GetAxis(Axis.VERTICAL_AXIS) < 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_Speed * Time.deltaTime);

        }
        else
        {
            // if no input to move character
            charController.Move(Vector3.zero);
        }

        

    }

        void Rotate()
        {
        Vector3 rotation_Direction = Vector3.zero;

                if(Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0)
            {
                rotation_Direction = transform.TransformDirection(Vector3.left);
            } 

            if(Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0)
            {
                rotation_Direction = transform.TransformDirection(Vector3.right);
            }

            if(rotation_Direction != Vector3.zero)
            {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_Direction), rotateDegreesPerSecond * Time.deltaTime);
            }

        }

    void AnimateWalk()
    {

        if(charController.velocity.sqrMagnitude != 0f)
        {
            playerAnimations.Walk(true);
              
                    
                        
        } else
        {
            playerAnimations.Walk(false);
          
        }

        


    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        

    }


























    
    }
