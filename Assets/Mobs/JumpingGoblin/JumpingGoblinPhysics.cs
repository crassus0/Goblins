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
    public void SetJump()
    {
        isJumping = true;
    }
}
