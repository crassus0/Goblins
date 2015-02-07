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

public class GoblinLocomotion : BasicLocomotion
{
    public float m_kickCooldown = 1;
    public float m_kickEnergy = 20;
    float m_kickTime;
    protected override void MoveForward(float speed)
	{
        if (rigidbody2D.velocity.magnitude < 1 || Vector2.Dot(rigidbody2D.velocity, transform.right)<0)
           rigidbody2D.AddRelativeForce(new Vector2(speed, 0));
        
	}
    private void KeepBalance(float angle)
    {
        rigidbody2D.angularVelocity = angle;
        
        
    }
    private void StandUp(Vector2 normal)
    {
        float angle = Vector2.Angle(normal, Vector2.up);
        transform.eulerAngles=new Vector3(0,0,angle);
        //Debug.Log(transform.rotation.eulerAngles);
    }
    protected override void Kick(GameObject target)
    {
        if(m_kickTime>0)return;
        HitInfo info=new HitInfo();
        info.hitEnergy=m_kickEnergy;
        target.SendMessage("OnHit", info );
        rigidbody2D.velocity = -transform.right.normalized;
        m_kickTime = m_kickCooldown;
    }
    protected override void Update()
    {
        if (m_kickTime > 0)
            m_kickTime -= Time.deltaTime;
    }
}
