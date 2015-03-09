using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level : MonoBehaviour 
{
    public static Level CurrentLevel { get { return m_levelObject; } }
    static Level m_levelObject;
    public GameObject MainTower;
    public int ResourceAmount=100;
    public Text ResourceAmountDisplay;
    int m_mobNumber=1;
	// Update is called once per frame
    void Awake()
    {
        m_levelObject = this;
    }
	void Update () {
        if (MainTower == null)
            FinishLevel(false);
        if (m_mobNumber == 0)
            FinishLevel(true);
        ResourceAmountDisplay.text = ResourceAmount.ToString();
	}
    public void MobSpawned()
    {
        m_mobNumber++;
    }
    public void CreateObject(CreatableObject obj)
    {
        if(obj.Price>ResourceAmount)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            ResourceAmount -= obj.Price;
        }
    }
    
    public void OnObjectDestroyed(PhysicsObject obj)
    {
        ResourceAmount += obj.DestructionPrice;
    }
    public void MobDestroyed()
    {
        m_mobNumber--;
    }
    public void WavesFinished()
    {
        m_mobNumber--;
    }
    public void FinishLevel(bool isVictory)
    {
        
            Time.timeScale = 0;
        
        

    }
}
