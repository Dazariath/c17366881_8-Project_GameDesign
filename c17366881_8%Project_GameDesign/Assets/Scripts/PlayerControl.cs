using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Animator m_animator;
    public GameObject enemy;
    public Transform zombi;
    public Animator m_ane;
    public GameObject SpawnItem;
    public Transform SpawnPos;

    public int health = 5;
    int DestroyTime = 4;


    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_ane = enemy.GetComponent<Animator>();
        Instantiate(SpawnItem, SpawnPos);
    }

    void FixedUpdate()
    {

        Die();

        float zombidist = Vector3.Distance(zombi.position, transform.position);

        float m_turn = Input.GetAxis("Horizontal");
        m_animator.SetFloat("turn", m_turn);

        float m_forward = Input.GetAxis("Vertical");
        m_animator.SetFloat("forward", m_forward);


        bool m_jump = Input.GetButtonDown("Jump");

        if (m_jump)
        {
            m_animator.SetTrigger("Jump");
        }


        bool m_kick = Input.GetKeyDown(KeyCode.M);
        m_animator.SetBool("Kick", m_kick);

        bool m_punch = Input.GetKeyDown(KeyCode.V);
        m_animator.SetBool("Punching", m_punch);

        bool m_Crossp = Input.GetKeyDown(KeyCode.B);
        m_animator.SetBool("CrossPunch", m_Crossp);

        bool m_elbow = Input.GetKeyDown(KeyCode.N);
        m_animator.SetBool("Elbow", m_elbow);




        if ((Input.GetKeyDown(KeyCode.M)) && zombidist < 2)
        {

            m_kick = true;

            m_ane.SetTrigger("dead");
            
        }

        if ((Input.GetKeyDown(KeyCode.V)) && zombidist < 2)
        {

            m_punch = true;

            m_ane.SetTrigger("dead");

        }

        if ((Input.GetKeyDown(KeyCode.B)) && zombidist < 2)
        {

            m_Crossp = true;

            m_ane.SetTrigger("dead");

        }

        if ((Input.GetKeyDown(KeyCode.N)) && zombidist < 2)
        {

            m_elbow = true;

            m_ane.SetTrigger("dead");

            zombidist = 0;


        }
    }
    void Die()
    {
        DestroyTime = 5;

        if (health == 0)
        {
            m_animator.SetTrigger("death");

            Destroy(gameObject, DestroyTime);

            Debug.Log("you died");
        }
    }




}





