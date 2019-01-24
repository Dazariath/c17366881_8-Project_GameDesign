using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private int targetOffset = 0;

    private enum NPCState { CHASE, KILL };              //sets all 3 states
    private NPCState m_NPCState;                                //used to call the states

    [SerializeField]private NavMeshAgent m_NavMeshAgent;        //calls enemies navmesh

    private int m_CurrentWaypoint;                              //Sets the current waypoint
    private bool m_IsPlayerNear;                                //checks if player is near
    private Animator m_Animator;                                //calls enemy enemy animator

    [SerializeField] Transform m_playerdist;                              //contains the player's position

    private bool m_enemyDeath;                                  //reads whether enemy is dead or not 

    [SerializeField] Manager m_Manager;                         //calls the manager
    [SerializeField] float m_FieldOfView;                       //has the FOV for the enemy in the editor
    [SerializeField] float m_ThresholdDistance;                 //sets the distance the enemy needs to be withtin in order to reach the waypoint
    [SerializeField] private Transform[] m_Waypoints;           //sets number of waypoints
    [SerializeField] GameObject m_Player;                       //hass the player gameobject

    // Use this for initialization
    void Start()
    {
        m_NPCState = NPCState.CHASE;                           //sets patrol as the default
        m_NavMeshAgent = GetComponent<NavMeshAgent>();          //calls the enemies navmesh agent into the script
        m_Animator = GetComponent<Animator>();                  //gets animator into the script
        m_CurrentWaypoint = 0;                                  //sets the first waypoint as the current waypoint

        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = true;

        HandleAnimation();                                      //calls the handle animation function
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();                                          //changes NPC state depending on players position
        m_NavMeshAgent.nextPosition = transform.position;       //changes destination

        switch (m_NPCState)                                     //activate functions depending on the NPC state
        {
            case NPCState.CHASE:
                Chase();
                break;
            case NPCState.KILL:
                Kill();
                break;
            default:
                break;
        }

        FacePlayer();
    }

    void CheckPlayer()                                          //changes the state of the NPC if the Player is within or outside of range
    {
        if (m_NPCState == NPCState.CHASE)                       //if NPC is chasing ad the player is outside of range, patrol
        {
            m_Animator.SetFloat("Forward", 1);
        }
    }

    void Kill()                                                                         //Kill function
    {
        Debug.Log("Killing");                                                           //Prints Killing in Comments

        float range = Vector3.Distance(m_playerdist.position, transform.position);      //sets float of the distance between player and enemy

        if (range <= 2.9)                                                               //if the player is within range the attack animation if
        {
            m_Animator.SetTrigger("LightLeft");
            m_NavMeshAgent.SetDestination(m_Player.transform.position);
        }
        else
        {
            m_NPCState = NPCState.CHASE;                                               //sets the NPC state to Patrol once the player is outside of range
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (m_playerdist.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((direction.x) + targetOffset, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Chase()                                                                        //
    {
        //*
        float range = Vector3.Distance(m_playerdist.position, transform.position);

        if (range <= 2.9)
        {
            m_NPCState = NPCState.KILL;
        }
        else
        {
            m_NPCState = NPCState.CHASE;
        }
        //
    }

    bool CheckFieldOfView()                                                                         //sets the Check FOV function
    {
        Vector3 direction = m_Player.transform.position - this.transform.position;
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, direction)).eulerAngles;


        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;                                           
        else if (angle.y < -180.0f) angle.y = angle.y + 360.0f;


        if (angle.y < m_FieldOfView / 2)
        {
            return true;
        }

        return false;
    }

    bool CheckOclusion()                                                                        //checks if an object is between the player and the enemy
    {
        RaycastHit hit;
        Vector3 direction = m_Player.transform.position - transform.position;

        if (Physics.Raycast(this.transform.position, direction, out hit))
        {
            if (hit.collider.gameObject == m_Player)                                            //if the reycast is not interrupted then it returns true
            {
                return true;
            }
        }
        return false;
    }
    //*/

    void CheckWaypointDistance()                                                //checks the distance to the next waypoint
    {
        if (Vector3.Distance(m_Waypoints[m_CurrentWaypoint].position, this.transform.position) < m_ThresholdDistance)
        {
            m_CurrentWaypoint = (m_CurrentWaypoint + 1) % m_Waypoints.Length;
        }
    }

    private void OnTriggerStay(Collider other)                  //bool is true if player is near
    {
        if (other.tag == "Player")
        {
            m_IsPlayerNear = true;

        }
    }

    private void OnTriggerExit(Collider other)                  //bool is false if the player is far away
    {
        if (other.tag == "Player")
        {
            m_IsPlayerNear = false; ;
        }
    }

    private void OnCollisionStay(Collision collision)           //Player takes damage if enemy colliders with them
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Manager.DecreaseHealth();
        }
    }

    private void OnDrawGizmos()                                         //draws wireframe of FOV, raycast and trigger
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);

        Gizmos.color = Color.red;
        Vector3 direction = m_Player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, direction);

        Vector3 rightDirection = Quaternion.AngleAxis(m_FieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 leftDirection = Quaternion.AngleAxis(-m_FieldOfView / 2, Vector3.up) * transform.forward;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rightDirection * 5.0f);
        Gizmos.DrawRay(transform.position, leftDirection * 5.0f);
    }

    void HandleAnimation()                          //enym runs if they are chasing and walks if not
    {

        if (m_NPCState == NPCState.CHASE)
        {
            m_Animator.SetFloat("Forward", 2);
        }

        /*float range = Vector3.Distance(m_playerdist.position, transform.position);

        if (range <= 3.0f)
        {
            m_Animator.SetTrigger("LegSweep");
        }
        */
    }
}

