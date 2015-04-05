using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasicCannonBall : PhysicsObject {
    public override int DestructionPrice {get { return 0; } }
    public GameObject Parent { get; set; }
    public float Damage = 100;
    List<GameObject> m_targets = new List<GameObject>();
    Animator m_animator;
    int m_hitHash = Animator.StringToHash("Hit");
    int m_destroyedNameHash = Animator.StringToHash("Base Layer.Destroyed");
    protected override void Start()
    {
        m_animator = transform.GetChild(0).GetComponent<Animator>();
    }
    public void SetPosition(Vector2 position)
    {
        position.x -= transform.GetChild(0).GetComponent<Renderer>().bounds.extents.x;
        position.y += transform.GetChild(0).GetComponent<Renderer>().bounds.extents.y;

        transform.position = position;
    }
    public void Aim(GameObject target, float ySpeed)
    {
        float d = ySpeed * ySpeed + 2 * (target.transform.position.y - transform.position.y) * Physics2D.gravity.y;
        float t = -(ySpeed + Mathf.Sqrt(d)) / Physics2D.gravity.y;
        float xSpeed = (target.transform.position.x - transform.position.x) / t + target.GetComponent<Rigidbody2D>().velocity.x;
        rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
    }
    void Update()
    {
        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);

        if (info.fullPathHash == m_destroyedNameHash)
            Destroy(gameObject);
    }
    

    protected override void FixedUpdate()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        m_animator.SetBool(m_hitHash, true);
        GetComponent<Rigidbody2D>().isKinematic = true;
        HitInfo info = new HitInfo();
        info.hitEnergy=Damage;
        foreach(GameObject x in m_targets)
        {
            if(x!= null)
                x.SendMessage("OnHit", info);
        }
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger)
            m_targets.Add(collision.gameObject);
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        m_targets.Remove(collision.gameObject);
    }
}
