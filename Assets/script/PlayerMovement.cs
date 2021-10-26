using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemylayers;


    public CharacterController controller;
    public float runSpeed = 40f;
    public BoxCollider2D idleCollider;
    public BoxCollider2D RightCollider;
    public BoxCollider2D LeftCollider;

    public bool canAttack = false;
    public bool IsStealth;
    public float runNum;
    float horizontalMove = 0f;
    bool jump = false;
    bool Fall = false;
    public bool IsAttack = false;
    public bool IsRuningRight = false;
    public Animator animator;
    Vector3 vc;
    float DownForce;
    void Start()
    {
        animator.SetBool("IsStealth", false);
        

    }

    // Update is called once per frame
    void Update()
    {
        
        vc = this.GetComponent<Rigidbody2D>().velocity;
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        runNum = horizontalMove;
        if (vc.y < 0)
        {
            Fall = true;
            animator.SetBool("IsFalling" ,true);
        }else if (vc.y == 0)
        {
            Fall = false;
            animator.SetBool("IsFalling", false);
        }
        if (runNum > 0)
        {
            IsRuningRight = true;
           // idleCollider.enabled = false;
           // RightCollider.enabled = true;
        }
        else if (runNum < 0)
        {
            IsRuningRight = false;
          //  idleCollider.enabled = false;
           // LeftCollider.enabled = true;

        }
        else if (runNum == 0)
        {
           
          //  idleCollider.enabled = true;
           // RightCollider.enabled = false;
           // LeftCollider.enabled = false;

        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump") && IsStealth !=true)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (IsAttack== false &&IsStealth ==false && vc.y==0) {
                
                IsStealth = true;
                this.GetComponent<PlayerMovement>().runSpeed = 0;
                animator.SetTrigger("PreSteath");
            animator.SetBool("IsStealth", true);
                
            }
            else if (IsStealth == true)
            {
                IsStealth = false;
                
                animator.SetBool("IsStealth", false); 
                this.GetComponent<PlayerMovement>().runSpeed = 80;
            }




        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsStealth == false && vc.y == 0 && IsAttack==false && canAttack ==true)
            {

                IsAttack = true;
                
                this.GetComponent<PlayerMovement>().runSpeed =0;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("hit" + enemy.name);
                    enemy.GetComponent<EnemyBehavior>().isHit = true;
                }
                animator.SetTrigger("IsAttack");
                animator.SetBool("IsAttackk",true);
                
            }
           




        }



    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void OnLanding()
    {
        Fall = true;
        animator.SetBool("IsJumping", false);
    }

    public void AttackFinish()
    {
        IsAttack = false;
        this.GetComponent<PlayerMovement>().runSpeed = 80;
        animator.SetBool("IsAttackk", false);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
