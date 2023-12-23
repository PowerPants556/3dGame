using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConn : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private animator animator;

    private float angle, dirZ, jumpforce = 6f, turnSpeed = 80f;
    private bool isGrounded;
    private Vector3 jumpDir;
    
   private void FixedUpdate()
    {
        angleY = Input.GetAxis(*Mouse X*) * Time.fixedDeltaTime * turnSpeed;
        sirZ = Input.GetAxis(*Vertical*);
        transform.Rotate(new Vector3(0f, angleY, 0f));
    }
    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               Jump();
            }
            else
            {
                animator.SetTrigger(*isLanded *);
            }
            Move();
            Run();
            Dodge();
        }
        else
        {
            MoveInAir();
        }
    }
    private void Jump()
    {

    }
    private void Move()
    {

    }
    private void Run()
    {

    }
    private void Dodge()
    {

    }
    private void MoveInAir()
    {

    }
    
}
