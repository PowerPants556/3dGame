using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region RB_VARIABLES
    //[SerializeField]
    private Rigidbody rb;

    private const float SPEED = 4f;
    private float moveX, moveZ, moveY = 0f;
    #endregion
    [SerializeField]
    private CharacterController controller;
    private Vector3 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MovingTransformPosition();
        //MovingTransformTranslate();
        MovingWithCharacterConn();
        //MovingVelocity();
    }

    private void MovingTransformPosition()
    {
        
        if (Input.GetKey(KeyCode.W))
        { 
            transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.z + 0.01f );
        }
    }
    private void MovingTransformTranslate()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);
        }
    }
    private void MovingVelocity()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(moveX, moveY, moveZ) * SPEED;
    }


    private void MovingWithCharacterConn()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveY = 0.15f;
            }
            else
            {
                moveY = 0f;
            }
        }
        else
        {
            moveY -= 1f * Time.fixedDeltaTime;
        }

        moveDir = new Vector3(moveX * Time.fixedDeltaTime, moveY, moveZ * Time.fixedDeltaTime);
        controller.Move(moveDir * SPEED);
        
    }
}
