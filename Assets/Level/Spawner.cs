using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {

    
    public List<MobWave> MobWaves =  new List<MobWave>();
    float m_timeLeft;
    void Spawn(GameObject mob)
    {

        GameObject x = Instantiate<GameObject>(mob) ;
        x.transform.parent = transform;
        x.transform.position = new Vector3(CameraControls.m_margins/2, TerrainControls.TerrainHeight(CameraControls.m_margins/2)+x.GetComponentInChildren<Renderer>().bounds.extents.y, 10);
    }

    void Update()
    {
        m_timeLeft -= Time.deltaTime;
        if(m_timeLeft<=0&&MobWaves.Count>0&&MobWaves[0].Mobs.Count>0)
        {
            Spawn(MobWaves[0].Mobs[0].MobPrefab);
            m_timeLeft += MobWaves[0].Mobs[0].MobCooldown;
            MobWaves[0].Mobs.RemoveAt(0);
            while (MobWaves[0].Mobs.Count == 0) 
            {
                
                MobWaves.RemoveAt(0);
                if (MobWaves.Count == 0)
                {
                    Level.CurrentLevel.WavesFinished();
                    break;
                }
                else
                    m_timeLeft += MobWaves[0].Cooldown;
                
            } 
            
        }
    }
    void Reset()
    {

    }
}
