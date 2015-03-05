using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {

    
    public List<MobWave> MobWaves =  new List<MobWave>();
    float m_timeLeft;
    void Spawn(GameObject mob)
    {
        Vector3 coords = new Vector3(CameraControls.m_margins/2, TerrainControls.TerrainHeight(CameraControls.m_margins/2)+mob.GetComponent<Renderer>().bounds.extents.y, 10);
        GameObject x =Instantiate(mob, coords, Quaternion.identity) as GameObject;
        x.transform.parent = transform;
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
