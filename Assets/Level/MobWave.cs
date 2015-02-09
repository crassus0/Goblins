using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
[System.Serializable]
public class MobWave
{
    public string Name = "Wave";
    public int Cooldown=30;
    public List<WaveMob> Mobs = new List<WaveMob>();
    
    
}
[System.Serializable]
public class WaveMob
{
    public GameObject MobPrefab;
    public float MobCooldown = 1;
}