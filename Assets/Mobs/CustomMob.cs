using UnityEngine;
using System.Collections;

public abstract class CustomMob : PhysicsObject 
{


    protected override void OnDestroy()
    {
        base.OnDestroy();
        Level.CurrentLevel.MobDestroyed();
    }
}
