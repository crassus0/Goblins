using UnityEngine;
using System.Collections;

public class JumpingGoblinPhysics : GoblinPhysics
{
    bool isJumping;
    protected override void FixedUpdate()
    {
        isJumping = false;
        base.FixedUpdate();
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (isJumping)
            m_energy = Mass * GetComponent<Rigidbody2D>().velocity.SqrMagnitude() / 2;
        
        base.OnCollisionExit2D(collision);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.contacts[0].normal.y > 0 && rigidbody2D.velocity.y < JumpingGoblinFloatStrategy.ParachuteOpenSpeed))
        { base.OnCollisionEnter2D(collision); }
    }
    public void SetJump()
    {
        isJumping = true;
    }
}
