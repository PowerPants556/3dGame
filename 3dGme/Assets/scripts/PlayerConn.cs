using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConn : MonoBehaviour
{
    public static PlayerConn instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hip;
    [SerializeField] private GameObject rightHand;
    public GameObject sword;
    private bool isSwordEquipd = false;
    [SerializeField] private EnemyConn enemy;


    private float angleY, dirZ, jumpforce = 6f, turnSpeed = 300f;
    private bool isGrounded;
    private Vector3 jumpDir;

    private float LastAttackTime = 0;

    private Vector3 swordStartRotation = Vector3.zero;
    private Vector3 swordStartPos = Vector3.zero;
    [HideInInspector]
    public bool isAttacking = false;

    [SerializeField] private Joystick joystickL, joystickR;

    private void Start()
    {
        instance = this;
        swordStartPos = sword.transform.localPosition;
        swordStartRotation = sword.transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        //angleY = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * turnSpeed;
        angleY = joystickR.Horizontal * Time.fixedDeltaTime * turnSpeed;
        //dirZ = Input.GetAxis("Vertical");
        dirZ = joystickL.Vertical;
        transform.Rotate(new Vector3(0f, angleY, 0f));
    }
    private void Update()
    {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack_R");
        
        if (isGrounded)
        {
            animator.SetTrigger("isLanded");
            Move(dirZ, "isWalkForward", "isWalkBack");
            Run();
            Dodge();
            //UnequipSword();
            //SwordActivate();
        }
        else
        {
            MoveInAir();
        }
        //UnequipSword();
    }
    public void Jump()
    {
        if (isGrounded)
        {
            animator.Play("Sword_Jump_Platformer_Start");
            animator.applyRootMotion = false;
            jumpDir = new Vector3(0f, jumpforce, dirZ * jumpforce / 2f);
            jumpDir = transform.TransformDirection(jumpDir);
            rb.AddForce(jumpDir * 50, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void Move(float dir, string parametrName, string altParamName)
    {
        if (dir > 0.3f)
        {
            animator.SetBool(parametrName, true);
        }
        else if (dir < -0.3f)
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
        if (joystickL.Vertical > 0.9f || joystickL.Vertical < -0.9f || Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
    private void Dodge()
    {
        if (joystickL.Horizontal < -0.8f || Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Sword_Dodgle_Left");
        }
        else if (joystickL.Horizontal > 0.8f || Input.GetKeyDown(KeyCode.D))
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

    private void SwordEquip()
    {
        sword.transform.SetParent(GameObject.Find("RightHand").transform);
    }
    private void SwordActivate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSwordEquipd)
            {
                animator.Play("Equip");
                isSwordEquipd = true;
                LastAttackTime = Time.time;
            }
            else
            {
                animator.Play("Sword_Attack_R");
                LastAttackTime = Time.time;
            }
        }
    }

    private  void UnequipSword()
    {
        if (isSwordEquipd && Time.time > LastAttackTime + 5f)
        {
            animator.Play("Sword_Holster");
            isSwordEquipd = false;
        }
    }

    private void SwordHolster() 
    {
        sword.transform.SetParent(GameObject.Find("Hips").transform);
        sword.transform.localPosition = swordStartPos;
        sword.transform.localEulerAngles = swordStartRotation;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Weapon" && enemy.isAttacking)
        {
            animator.Play("Sword_Shield_Hit_L_2");
        }
    }

    public void OnButtonAttackCicked()
    {
        if (isGrounded)
        {
            if (!isSwordEquipd)
            {
                animator.Play("Equip");
                isSwordEquipd = true;
                LastAttackTime = Time.time;
            }
            else
            {
                animator.Play("Sword_Attack_R");
                LastAttackTime = Time.time;
            }
        }
    }
}
