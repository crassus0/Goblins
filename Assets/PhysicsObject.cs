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

public abstract class PhysicsObject : MonoBehaviour
{
    protected new Rigidbody2D rigidbody2D { get { return m_rigidbody; } }
    Rigidbody2D m_rigidbody;
    public float Mass { get { return rigidbody2D.mass; } }
    public float Strength;
    public abstract int DestructionPrice { get; }
    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    protected abstract void Start();

    protected abstract void FixedUpdate();

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    protected abstract void OnCollisionExit2D(Collision2D collision);

    public virtual void OnHit(HitInfo info)
    {
        Strength -= info.hitEnergy;
        if (Strength <= 0)
            Destroy(gameObject);
    }
    protected virtual void OnDestroy()
    {
        Level.CurrentLevel.OnObjectDestroyed(this);
    }
    public virtual float FindMaxHeight()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Vector2 raycastOrigin = transform.position;
        float maxHeight = collider.bounds.max.y;
        IEnumerable<RaycastHit2D> hits = Physics2D.RaycastAll(raycastOrigin, Vector2.up, 1000, Constants.RaycastMaskPhysics);
        if (hits.Any())
        {
            float max = hits.Max(x => x.point.y);
            maxHeight = max > maxHeight ? max : maxHeight;
        }
        raycastOrigin.x = collider.bounds.min.x + 0.01f;
        hits = Physics2D.RaycastAll(raycastOrigin, Vector2.up, 1000, Constants.RaycastMaskPhysics);
        if (hits.Any())
        {
            float max = hits.Max(x => x.point.y);
            maxHeight = max > maxHeight ? max : maxHeight;
        }
        raycastOrigin.x = collider.bounds.max.x - 0.01f;
        hits = Physics2D.RaycastAll(raycastOrigin, Vector2.up, 1000, Constants.RaycastMaskPhysics);
        if (hits.Any())
        {
            float max = hits.Max(x => x.point.y);
            maxHeight = max > maxHeight ? max : maxHeight;
        }
        return maxHeight;
    }
    public virtual Bounds GetExtents()
    {
        return GetComponent<Collider2D>().bounds;
    }
}
public struct HitInfo
{
    public Vector2 velocity;
    public float mass;
    public float hitEnergy;

}

