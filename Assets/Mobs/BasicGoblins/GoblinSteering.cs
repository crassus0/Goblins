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
public class GoblinSteering : BasicSteering
{
    public ContactPoint2D SurfaceContact { get; set; }
    public List<GameObject> Targets { get; set; }
    public bool Stopped { get; set; }
    
    HashSet<GameObject> m_terrain = new HashSet<GameObject>();
    //float m_colliderRatio;
    protected override void Start()
    {
        Level.CurrentLevel.MobSpawned();
        //m_colliderRatio = GetComponent<Collider2D>().bounds.extents.y / GetComponent<Collider2D>().bounds.extents.x;
        SurfaceContact = new ContactPoint2D();
        //SurfaceContact.normal = Vector2.up;
        Targets = new List<GameObject>();
        Stopped = false;
        SetStrategy(GoblinFloatStrategy.Instance());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            if (Mathf.Abs(point.normal.x) < point.normal.y)
            {
                SurfaceContact = point;
                if (!m_terrain.Contains(collision.gameObject))
                    m_terrain.Add(collision.gameObject);
                break;
            }

        }
    }



    protected void OnCollisionExit2D(Collision2D collision)
    {
        m_terrain.Remove(collision.gameObject);
        if (m_terrain.Count == 0)
        {
            SurfaceContact = new ContactPoint2D();
        }

    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D realContact = new ContactPoint2D();
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Physics2D.Raycast(contact.point, -transform.up, 0.1f).collider != null)
                realContact = contact;
        }
        if (realContact.normal != default(Vector2))
        {
            if (!m_terrain.Contains(collision.gameObject))
            {
                m_terrain.Add(collision.gameObject);

            }
            SurfaceContact = realContact;
        }
        else
        {
            m_terrain.Remove(collision.collider.gameObject);
            if (m_terrain.Count == 0)
            {
                SurfaceContact = new ContactPoint2D();
            }
        }
    }
}

