﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasicCannonBall : PhysicsObject {
    public override int DestructionPrice {get { return 0; } }
    public GameObject Parent { get; set; }
    public float Damage = 100;
    List<GameObject> m_targets = new List<GameObject>();
    Animator m_animator;
    int m_hitHash = Animator.StringToHash("Hit");
    int m_destroyedNameHash = Animator.StringToHash("Base Layer.Destroyed");
    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    void Update()
    {
        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);

        if (info.nameHash == m_destroyedNameHash)
            Destroy(gameObject);
    }
    

    protected override void FixedUpdate()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        m_animator.SetBool(m_hitHash, true);
        rigidbody2D.isKinematic = true;
        HitInfo info = new HitInfo();
        info.hitEnergy=Damage;
        foreach(GameObject x in m_targets)
        {
            if(x!= null)
                x.SendMessage("OnHit", info);
        }
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger)
            m_targets.Add(collision.gameObject);
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        m_targets.Remove(collision.gameObject);
    }
}
