using UnityEngine;
using System.Collections;

public class CatapultVisualizer : MonoBehaviour 
{
    public GameObject Shell;
    Animator m_animator;
    static int m_shootHash = Animator.StringToHash("Shoot");
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        m_animator.SetTrigger(m_shootHash);
    }

    
}
