using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUserControl : MonoBehaviour
{

    private float m_turn;                                   //Turn and Forward Floats
    private float m_forward;
    private bool m_jump;                                    //Jump Trigger
    private bool m_sprint;                                  //Sprint Bool

    private bool m_BlockAttack;                             //Block Bool
    private bool m_Hpunch;                                  //punch and kick attack triggers
    private bool m_Rpunch;
    private bool m_LeftKick;
    private bool m_RightKick;

    private NewCharacterController m_character;             //gets character conroller

    private void Start()
    {
        m_character = GetComponent<NewCharacterController>();       //gets character controller in the script
    }
    // Use this for initialization

    void FixedUpdate()
    {
        // Get Inputs
        m_turn = Input.GetAxis("Horizontal");
        m_forward = Input.GetAxis("Vertical");
        m_jump = Input.GetButtonDown("Jump");
        m_sprint = Input.GetButton("Fire1");

        m_BlockAttack = Input.GetKey(KeyCode.I);                                        //sets trigger to true as long as I is held down
        m_Hpunch = Input.GetKeyDown(KeyCode.O);                                         //sets bools to true once these keys are pressed once
        m_Rpunch = Input.GetKeyDown(KeyCode.U);
        m_LeftKick = Input.GetKeyDown(KeyCode.J);
        m_RightKick = Input.GetKeyDown(KeyCode.L);

        m_character.Move(m_turn, m_forward, m_jump, m_sprint);                              //establshed the Move function in the User control script

        m_character.Combat(m_Hpunch, m_BlockAttack, m_LeftKick, m_Rpunch, m_RightKick);     //establshed the Combat function in the User control script
    }
}
