﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class JumpingGoblinLocomotion : GoblinLocomotion
{
    public Animator m_animator;
    public float m_maxJumpSpeed = 1.5f ;
    int m_parachuteOpenTrigger = Animator.StringToHash("ParachuteOpenTrigger");
    int m_parachuteCloseTrigger = Animator.StringToHash("ParachuteCloseTrigger");
    bool m_parachuteOpen = false;
	public virtual void Jump(Vector2 direction)
	{
        if (direction.magnitude > m_maxJumpSpeed)
            direction = direction.normalized * m_maxJumpSpeed;
        GetComponent<Rigidbody2D>().velocity += direction;
        GetComponent<JumpingGoblinPhysics>().SetJump();

	}
    public virtual void Break(float k)
    {
      GetComponent<Rigidbody2D>().AddForce(-GetComponent<Rigidbody2D>().velocity*k);
    }
    public virtual void StabilizeAngle()
    {
        float angle = transform.rotation.eulerAngles.z;
        if (angle > 180)
            angle -= 360;
        if(-Mathf.Sign(angle)*GetComponent<Rigidbody2D>().angularVelocity<20)
            GetComponent<Rigidbody2D>().AddTorque(-angle * 0.01f);
    }
    public void ParachuteOpen()
    {
        if (m_parachuteOpen) return;
        m_animator.SetTrigger(m_parachuteOpenTrigger);
        m_parachuteOpen = true;

    }

    public void ParachuteClose()
    {
        if (!m_parachuteOpen) return;
        m_animator.SetTrigger(m_parachuteCloseTrigger);
        m_parachuteOpen = false;
    }
}
