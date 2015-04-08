using UnityEngine;
using System.Collections;

public class CatapultVisualizer : MonoBehaviour 
{
    public GameObject Shell;
    Animator m_animator;
    static int m_shootHash = Animator.StringToHash("CatapultShoot");
    static int m_aimHash = Animator.StringToHash("CatapultAim");
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        m_animator.SetTrigger(m_shootHash);
    }
    public void Aim()
    {
        m_animator.SetTrigger(m_aimHash);
    }
    
}
