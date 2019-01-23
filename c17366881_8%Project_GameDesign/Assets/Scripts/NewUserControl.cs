using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUserControl : MonoBehaviour
{

    private float m_turn;                                   //Turn and Forward Floats
    private float m_forward;

    private bool m_BLeft;                             //Block float
    private bool m_BRight;
    private bool m_LRight;                                  //punch and kick attack triggers
    private bool m_LLeft;
    private bool m_HeadB;

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
        m_forward = Input.GetAxis("Vertical");                      //sets trigger to true depending on where the mouse is and what button you press

        m_BLeft = Input.GetKeyDown(KeyCode.J);
        m_BRight = Input.GetKeyDown(KeyCode.L);        
        m_BRight = Input.GetKeyDown(KeyCode.L);        
                                      
        m_HeadB = Input.GetKeyDown(KeyCode.I);                                         //sets bools to true once these keys are pressed once
        m_LRight = Input.GetKeyDown(KeyCode.O);
        m_LLeft = Input.GetKeyDown(KeyCode.U);

        m_character.Move(m_turn, m_forward);                              //establshed the Move function in the User control script

        m_character.Combat(m_BLeft, m_BRight, m_LLeft, m_LRight, m_HeadB);     //establshed the Combat function in the User control script
    }
}
