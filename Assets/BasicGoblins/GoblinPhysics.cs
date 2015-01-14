using UnityEngine;
using System.Collections;

public class GoblinPhysics : PhysicsObject {
    float m_energy;
    protected override void Start()
    {
    }
    protected override void FixedUpdate()
    {
        m_energy = Mass * rigidbody2D.velocity.SqrMagnitude() / 2;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        float hitEnergy = Mathf.Abs(m_energy - Mass * rigidbody2D.velocity.SqrMagnitude() / 2);
        if (hitEnergy > 1)
        {
            Strength -= hitEnergy;
            HitInfo info = new HitInfo();
            info.velocity = rigidbody2D.velocity;
            info.mass = Mass;
            info.hitEnergy = hitEnergy;
            collision.collider.SendMessage("OnHit", info);
        }
        if (Strength <= 0)
            Destroy(gameObject);
        m_energy = Mass * rigidbody2D.velocity.SqrMagnitude() / 2;
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        float hitEnergy = Mathf.Abs(m_energy - Mass * rigidbody2D.velocity.SqrMagnitude() / 2);
        if (hitEnergy > 1)
        {
            Strength -= Mathf.Abs(m_energy - Mass * rigidbody2D.velocity.SqrMagnitude() / 2);
        }
        //m_energy = 0;
        if (Strength <= 0)
            Destroy(gameObject);
    }
    protected override void OnHit(HitInfo info)
    {

    }
}
