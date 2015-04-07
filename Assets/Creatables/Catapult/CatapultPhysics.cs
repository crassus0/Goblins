using UnityEngine;
using System.Collections;

public class CatapultPhysics : CreatableObject
{
    public override string IconName
    {
        get { throw new System.NotImplementedException(); }
    }

    public override int Price
    {
        get { return 100; }
    }

    public override int DestructionPrice
    {
        get { return 0; }
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

    protected override void Start()
    {
    }
}
