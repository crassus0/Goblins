using UnityEngine;
using System.Collections;

public class GoblinPhysics : PhysicsObject {
    protected float m_energy{get;set;}
    protected override void Start()
    {
    }
    protected override void FixedUpdate()
    {
        m_energy = Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        float hitEnergy = Mathf.Abs(m_energy - Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2);
        if (hitEnergy > 1)
        {
            Strength -= hitEnergy;
            HitInfo info = new HitInfo();
            info.velocity = GetComponent<Rigidbody2D>().velocity;
            info.mass = Mass;
            info.hitEnergy = hitEnergy;
            collision.collider.SendMessage("OnHit", info);
        }
        if (Strength <= 0)
            Destroy(gameObject);
       
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        float hitEnergy = Mathf.Abs(m_energy - Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2);
        if (hitEnergy > 1)
        {
            Strength -= Mathf.Abs(m_energy - Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2);
        }
        //m_energy = 0;
        if (Strength <= 0)
            Destroy(gameObject);
    }
    protected override void OnHit(HitInfo info)
    {
        Strength -= info.hitEnergy;
        if (Strength <= 0)
            Destroy(gameObject);
    }
    public override int DestructionPrice
    {
        get { return 20; }
    }
}
