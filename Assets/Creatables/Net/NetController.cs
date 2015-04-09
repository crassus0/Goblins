using UnityEngine;
using System.Collections;

public class NetController : CreatableObject {
    public NetVisualizer m_visualizer;
    public NetSegment[] segments;
    protected override void Start()
    {

    }
    public override string IconName
    {
        get { throw new System.NotImplementedException(); }
    }

    public override int Price
    {
        get { throw new System.NotImplementedException(); }
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


}
