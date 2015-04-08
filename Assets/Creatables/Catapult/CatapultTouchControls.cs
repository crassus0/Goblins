using UnityEngine;
using System.Collections;

public class CatapultTouchControls : MonoBehaviour 
{
    float m_torque = 0;
    bool m_pointerDown = false;
    public GameObject CannonballPrefab;
    public CatapultVisualizer Visualizer;
    
    public Vector3 ShootStartPosition= new Vector3(0, 1.5f);
    public Vector2 ShootDirection = new Vector2(-6f, 1f).normalized;
    public float ShootDelay = 0.1f;
    public float RechargeDelay;
    static readonly float s_minSpeed = 4;
    static readonly float s_maxSpeed = 15;
    static readonly float s_maxTime = 1;
    float m_delay = -1;
    float m_rechargeTime=0;
	public void OnPointerDown()
    {
        m_pointerDown = true;
    }
    public void OnPointerUp()
    {
        if (m_rechargeTime > 0) return;
        m_delay = ShootDelay;
        m_pointerDown = false;
        Visualizer.Shoot();
    }
	// Update is called once per frame
	void Update () 
    {
        if (m_rechargeTime > 0)
        {
            m_rechargeTime -= Time.deltaTime;
        }
        else if (m_pointerDown)
        {
            if (m_torque < s_maxTime)
                m_torque += Time.deltaTime;
            else
                m_torque = s_maxTime;
        }
        else if (m_delay > 0)
        {
            m_delay -= Time.deltaTime;

        }
        else if (m_delay > -1)
        {
            m_delay = -1;
            Shoot();
        }
	}
    void Shoot()
    {

        m_rechargeTime = RechargeDelay;
        GameObject cannonBall = Instantiate(CannonballPrefab);
        cannonBall.transform.position = ShootStartPosition+transform.position;
        Vector2 cannonballVelocity = ShootDirection*(m_torque * (s_maxSpeed - s_minSpeed) / s_maxTime + s_minSpeed);
        cannonBall.GetComponent<Rigidbody2D>().velocity =  cannonballVelocity;
        
        m_torque = 0;

        //Debug.Break();
    }
}
