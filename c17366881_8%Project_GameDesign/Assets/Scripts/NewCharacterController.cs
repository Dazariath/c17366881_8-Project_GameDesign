using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewCharacterController : MonoBehaviour {

    [SerializeField] float m_EnemyHealth = 50.0f;               //Enemy Health stat

    private Animator m_animator;                                //Player animator

    private Rigidbody m_rb;                                     //Player RigidBody

    public GameObject m_enemy;                                  //enemy GameObject
    public Transform m_enemyTrans;                              //EnemyPosition
    public Animator m_EnemyAnim;                                //EnemyAnimator

    public float EnemyCloseness = 50;                           //Distance from enemy to Player

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();                  //Gets Player Animator
    }    

    public void Move(float turn,float forward, bool jump, bool sprint) //Move Function with variables
    {

        m_animator.SetFloat("turn", turn);                              //turn and forward floats for movement
        m_animator.SetFloat("forward", forward);

        if (jump)                                                       //activates jump trigger
        {
            m_animator.SetTrigger("Jump");
        }

        if (sprint)                                                     //activates sprinting and deactivates it once CNTRL is released
        {
            m_animator.SetBool("IsSprinting", true);
        }
        else
        {
            m_animator.SetBool("IsSprinting", false);
        }

        //Sprinting();
    }

    public void EnemyDead()                                             //Plays enemy death animation once the health is below 0
    {
        m_EnemyAnim.SetBool("IsDead", true);
    }

    public void EnemyLoseHealth()                                       //Checks if enemy is within rage and reduces health stat
    {
        bool enemyNear = false;
        
        float enemyDist = Vector3.Distance(m_enemyTrans.position, transform.position);

        if (enemyDist <= EnemyCloseness)                                //Checks if they are within range
        {
            enemyNear = true;
        }
        

        if (enemyNear)                                                  //Reduces health
        {
            m_EnemyHealth = m_EnemyHealth - 5;
        }

    }

    public void Combat(bool HPunch, bool BAttack, bool LKick, bool RPunch, bool RKick)                      //Combat Function with variables
    {
        if (LKick)                                  //Plays Left kick animation and reduces enemy health
        {
            m_animator.SetTrigger("LeftKick");
            EnemyLoseHealth();
        }

        if (RKick)                                  //Plays Right kick animation
        {
            m_animator.SetTrigger("RightKick");
            EnemyLoseHealth();
        }

        if (RPunch)                                 //Plays Left Hook animation
        {
            m_animator.SetTrigger("LeftHook");
            EnemyLoseHealth();
        }

        if (HPunch)                                 //Plays Right Hook animation
        {
            m_animator.SetTrigger("RightHook");
            EnemyLoseHealth();
        }

        if (BAttack)                                        //Plays Block animation and turns it off once the "I" key is released
        {
            m_animator.SetBool("BlockAttack", true);
        }
        else
        {
            m_animator.SetBool("BlockAttack", false);
        }

        if(m_EnemyHealth <= 0)                              //Kills enemy once dead
        {
            EnemyDead();
        }

    }

    

    //*/

    /*void Sprinting()
    {
        if (Input.GetKey("Fire1"))
        {
            m_sprint = 0.2f;
        }
        else
        {
            m_sprint = 0.0f;
        }
    }*/



}
