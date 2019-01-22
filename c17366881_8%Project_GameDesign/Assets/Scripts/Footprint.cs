using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour {

    [SerializeField] GameObject m_footprint;

    private Animator m_animator;
    private Transform m_rightFootTransform;
    private Transform m_leftFootTransform;

    private void Start()
    {
        m_animator = GetComponent<Animator>();

        m_rightFootTransform = m_animator.GetBoneTransform(HumanBodyBones.RightFoot);
        m_leftFootTransform = m_animator.GetBoneTransform(HumanBodyBones.LeftFoot);
    }

    public void MakeFootPrint(int scale)
    {
        Debug.Log("Footprint");

        if (scale > 0)// left
        {
            Instantiate(m_footprint, m_leftFootTransform.position, m_leftFootTransform.rotation);
        }
        else
        {
            Instantiate(m_footprint, m_rightFootTransform.position, m_rightFootTransform.rotation);
        }
    }

}
