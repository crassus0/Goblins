using UnityEngine;
using System.Collections;

public class NetController : CreatableObject {
    public NetVisualizer m_visualizer;
    public NetSegment[] m_segments;
    protected override void Start()
    {

    }
    public override string IconName
    {
        get { return "Net"; }
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
        //transform.position = segments[0].transform.position;
    }
    protected void Update()
    {

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }
    public override void Place()
    {
        foreach (NetSegment x in m_segments)
        {
            x.GetComponent<Collider2D>().isTrigger = false;
            x.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        Level.CurrentLevel.CreateObject(this);
    }

}
