using UnityEngine;
using System.Collections;

public class MineCourier : CustomMob 
{
    public GameObject Target;
    public int BearedPrice = 100;

    public override int DestructionPrice
    {
        get { return m_destructionPrice; }
    }
    int m_destructionPrice = 0;
    protected override void Start()
    {
    }

    protected override void FixedUpdate()
    {
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject==Target)
        {
            m_destructionPrice = BearedPrice;
            Destroy(gameObject);
        }

    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }
}
