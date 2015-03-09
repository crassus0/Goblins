using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasicTower : CreatableObject 
{
    public GameObject CannonballPrefab;
    public float ShootCooldown=10;
    float m_timeToShoot;
    List<GameObject> m_targets= new List<GameObject>();
    float ySpeed;
    BoxCollider2D m_hitCollider;
    public override int DestructionPrice { get { return 10; } }
   
    protected override void Start()
    {
        m_timeToShoot =  ShootCooldown;
        float range = transform.GetChild(0).GetComponent<CircleCollider2D>().radius;
        ySpeed = Mathf.Sqrt(-range * Physics2D.gravity.y);
        m_hitCollider = GetComponent<BoxCollider2D>();
       
    }
    protected override void FixedUpdate()
    {
    }
    public override void RotateOnCreation(float angle)
    {
        
    }
    public override void ScaleOnCreation(float scale)
    {
        
    }
    public override void DragOnCretaion(Vector2 position)
    {
        if (m_hitCollider == null)
            m_hitCollider = GetComponent<BoxCollider2D>();
        Vector3 newPosition = position;
        Vector2 raycastPoint = new Vector2(position.x - m_hitCollider.bounds.extents.x, 150);
        //gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint, -Vector2.up, 1000, Constants.RaycastMaskPhysics);
        float yCoord = hit.point.y;
        raycastPoint.x=  position.x;
        hit = Physics2D.Raycast(raycastPoint, -Vector2.up, Mathf.Infinity,1);
        yCoord=hit.point.y>yCoord?hit.point.y:yCoord;
        raycastPoint.x = position.x + m_hitCollider.bounds.extents.x;
        hit = Physics2D.Raycast(raycastPoint, -Vector2.up, Mathf.Infinity,1);
        yCoord = hit.point.y > yCoord ? hit.point.y : yCoord;

        newPosition.y = yCoord+m_hitCollider.size.y/2;
        transform.position = newPosition;
        //gameObject.layer = LayerMask.NameToLayer("Default");
    }
    public override void SetAngleOnCreation(float angle)
    {
    }
    public override void SetScaleOnCreation(float scale)
    {
    }
    void Update()
    {
        m_timeToShoot-=Time.deltaTime;
        
        if(m_timeToShoot<=0)
        {
            
            while (m_targets.Count >0&&(m_targets[0] == null))
                m_targets.Remove(m_targets[0]);
            if (m_targets.Count == 0)
                m_timeToShoot = 0;
            else
            {
                GameObject target=m_targets[0];
                m_timeToShoot += ShootCooldown;
                Vector2 cannonBallPosition = GetComponent<Renderer>().bounds.min;
                cannonBallPosition.y+=GetComponent<Renderer>().bounds.size.y;
                GameObject cannonBall = Instantiate(CannonballPrefab) as GameObject;
                cannonBall.GetComponent<BasicCannonBall>().SetPosition(cannonBallPosition);
                cannonBall.GetComponent<BasicCannonBall>().Aim(target, ySpeed);
               
                //cannonBall
            }
        }
    }
    public void AddTarget(GameObject target)
    {
        m_targets.Add(target);
    }
    public void RemoveTarget(GameObject target)
    {
        m_targets.Remove(target);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }

    


    public override void Place()
    {

        base.Place();
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    public override string IconName
    {
        get { return "watchtower"; }
    }

    public override int Price
    {
        get { return 100; }
    }
}
