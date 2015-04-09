using UnityEngine;
using System.Collections;

public class CatapultPhysics : CreatableObject
{
    BoxCollider2D m_hitCollider;
    public override string IconName
    {
        get { return "catapultIcon"; }
    }

    public override int Price
    {
        get { return 100; }
    }

    public override int DestructionPrice
    {
        get { return 0; }
    }
    public override void DragOnCretaion(Vector2 position)
    {
        if (m_hitCollider == null)
            m_hitCollider = GetComponent<BoxCollider2D>();
        Vector3 newPosition = position;
        Vector2 raycastPoint = new Vector2(position.x - m_hitCollider.size.x/2, 150);
        //gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint, -Vector2.up, 1000, Constants.RaycastMaskPhysics);
        Vector2 leftPoint = hit.point;
        raycastPoint.x = position.x + m_hitCollider.size.x/2;
        hit = Physics2D.Raycast(raycastPoint, -Vector2.up, 1000, Constants.RaycastMaskPhysics);
        Vector2 rightPoint = hit.point;
        newPosition = (rightPoint + leftPoint) / 2;
        newPosition.y += m_hitCollider.size.y / 2;
        transform.position = newPosition;
        transform.rotation = Quaternion.FromToRotation(Vector2.right, rightPoint - leftPoint);
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
