using UnityEngine;
using System.Collections.Generic;

public class NetController : CreatableObject {
    public NetVisualizer m_visualizer;
    public NetSegment[] m_segments;

    List<BasicLocomotion> m_nettedObjects = new List<BasicLocomotion>();
    List<int> m_nettingSegmentsCount = new List<int>();
    public void AddTarget(GameObject target)
    {
        BasicLocomotion locomotion = target.GetComponent<BasicLocomotion>();
        if(locomotion!=null)
        {
            int index = m_nettedObjects.IndexOf(locomotion);
            if (index < 0)
            {
                m_nettedObjects.Add(locomotion);
                m_nettingSegmentsCount.Add(0);
            }
            else
            {
                m_nettingSegmentsCount[index]++;
            }
        }
    }
    public void RemoveTarget(GameObject target)
    {
        BasicLocomotion locomotion = target.GetComponent<BasicLocomotion>();
        if (locomotion != null)
        {
            int index = m_nettedObjects.IndexOf(locomotion);
            if (index >= 0)
            {
                m_nettingSegmentsCount[index]--;
                if (m_nettingSegmentsCount[index] == 0)
                {
                    m_nettingSegmentsCount.RemoveAt(index);
                    m_nettedObjects.RemoveAt(index);
                }
            }
        }
    }
    protected override void Start()
    {

    }
    public override string IconName
    {
        get { return "Net"; }
    }

    public override int Price
    {
        get { return 20; }
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
        for(int i = 0; i<m_nettedObjects.Count; i++)
        {
            if(m_nettingSegmentsCount[i]>0)
            {
                m_nettedObjects[i].Stuppefy();
            }
        }
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
