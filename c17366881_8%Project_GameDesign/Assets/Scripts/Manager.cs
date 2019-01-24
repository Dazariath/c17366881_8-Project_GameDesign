using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Animator m_AnimPlayer;                               //player animator controller

    [SerializeField] float m_PlayerHealth = 100.0f;             //sets player health

    public Animator m_AnimEnemy;    //used to play enemy death

    // Use this for initialization
    void Start()
    {

    }

    public void DecreaseHealth()
    {
        if (Input.GetKey(KeyCode.I))                            //sets the block bool to true as long as I is held down
        {
            m_AnimPlayer.SetBool("BlockAttack", true);
            Debug.Log("Blocking");                              //prints Blocking in the console if Blocking is true
        }
        else
        {
            m_AnimPlayer.SetBool("BlockAttack", false);         //takes damage everytime the enemy is within range and the player does,nt block
            m_PlayerHealth = m_PlayerHealth - 1f;
            m_AnimPlayer.SetTrigger("TakeDamage");              //plays taking damage animation
            CheckHealth();
            Debug.Log(m_PlayerHealth);                          //prints the players health into the console
        }
    }

    private void CheckHealth()          //if the health reaches below 0 the it call the end game function
    {
        if (m_PlayerHealth <= 0)
        {
            GameOver();
        }
    }

    /*
    private void CheckEnemyHealth()
    {
        if (m_EnemyHealth <= 0)
        {
            EnemyDeath();
        }
    }
    */

    private void GameOver()             //ends game if player dies
    {
        SceneManager.LoadScene(0);
    }

}

