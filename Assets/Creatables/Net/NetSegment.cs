using UnityEngine;
using System.Collections;

public class NetSegment : PhysicsObject
{

    // Use this for initialization
    float m_size;
    public NetController m_parentNet;
    protected override void Start()
    {

        m_size = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void FixedUpdate()
    {
        if (rigidbody2D.velocity.y > 0)
        {
            rigidbody2D.AddForce(new Vector2(0, -rigidbody2D.velocity.y));
        }
    }
    public Vector2 GetLeftPoint()
    {
        m_size = GetComponent<BoxCollider2D>().size.x;
        float sizeX = m_size * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) / 2;
        float sizeY = m_size * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) / 2;
        Vector3 max = new Vector3(sizeX, sizeY);
        return transform.position - max;
    }
    public Vector2 GetRightPoint()
    {
        m_size = GetComponent<BoxCollider2D>().size.x;
        float sizeX = m_size * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) / 2;
        float sizeY = m_size * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) / 2;
        Vector3 max = new Vector3(sizeX, sizeY);
        return transform.position + max;
    }

    public override int DestructionPrice
    {
        get { return 0; }
    }
    protected override void OnDestroy()
    {
        Destroy(m_parentNet.gameObject);
        base.OnDestroy();

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        m_parentNet.AddTarget(collision.collider.gameObject);
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        m_parentNet.RemoveTarget(collision.collider.gameObject);
    }
}
