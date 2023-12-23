using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConn : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private float angleY, dirZ, jumpforce = 6f, turnSpeed = 80f;
    private bool isGrounded;
    private Vector3 jumpDir;

    private void FixedUpdate()
    {
        angleY = Input.GetAxis("MouseX") * Time.fixedDeltaTime * turnSpeed;
        dirZ = Input.GetAxis("Vertical");
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
                animator.SetTrigger("isLanded");
            }
            Move(dirZ, "isWalkForward", "isWalkBack");
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
        animator.Play("Sword_Jump_Platformer_Start");
        animator.applyRootMotion = false;
        jumpDir = new Vector3(0f, jumpforce, dirZ * jumpforce / 2f);
        jumpDir = transform.TransformDirection(jumpDir);
        rb.AddForce(jumpDir, ForceMode.Impulse);
        isGrounded = false;
    }
    private void Move(float dir, string parametrName, string altParamName)
    {
        if (dir > 0)
        {
            animator.SetBool(parametrName, true);
        }
        else if (dir < 0)
        {
            animator.SetBool(altParamName, true);
        }
        else
        {
            animator.SetBool(parametrName, false);
            animator.SetBool(altParamName, false);
        }
    }
    private void Run()
    {
        animator.SetBool("isRun", Input.GetKey(KeyCode.LeftShift));
    }
    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Sword_Dodgle_Left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("Sword_Dodge_Right");
        }
    }
    private void MoveInAir()
    {
        if (new Vector2(rb.velocity.x, rb.velocity.z).magnitude < 1.1f)
        {
            jumpDir = new Vector3(0f, rb.velocity.y, dirZ);
            jumpDir = transform.TransformDirection(jumpDir);
            rb.velocity = jumpDir;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        animator.applyRootMotion = true;
    }
}
