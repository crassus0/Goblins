using UnityEngine;
using System.Collections;

public class Brick : PhysicsObject {


    protected override void Start()
    {
    }

    protected override void FixedUpdate()
    {
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }

    protected override void OnHit(HitInfo info)
    {
        Strength -= info.hitEnergy;
        if (Strength <= 0)
            Destroy(gameObject);
    }
}
