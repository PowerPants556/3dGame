using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConn : MonoBehaviour
{

    private int health = 3;
    private float prevHitTime, ignoreDamageWindow = 1.5f;
    [SerializeField] private Animator animator;
    void Start()
    {
        prevHitTime = 0f;
    }

   private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Weapon" && Time.time > prevHitTime + ignoreDamageWindow)
        {
            health--;
            prevHitTime = Time.time;
            if (health > 1)
            {
                animator.Play("KnockdownRight");
            }
            else if(health < 1)
            {
                animator.Play("Sword_Defeat_1_Start");
            }
            else
            {
                animator.SetTrigger("isDead");
            }
        }
    }
}
