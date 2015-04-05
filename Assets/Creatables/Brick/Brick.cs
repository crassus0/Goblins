using UnityEngine;
using System.Collections;

public class Brick : CreatableObject {
    float m_energy;

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
        m_energy = Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2;
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }

    public override void ScaleOnCreation(float scale)
    {
    }
    public override void SetScaleOnCreation(float scale)
    {
    }
    public override string IconName
    {
        get { return "brick"; }
    }
    public override int Price
    {
        get { return 10; }
    }
    public override int DestructionPrice
    {
        get { return 2; }
    }
}
