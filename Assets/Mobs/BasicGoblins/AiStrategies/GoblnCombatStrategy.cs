using UnityEngine;
using System.Collections.Generic;

public class GoblnCombatStrategy : BasicSteeringStrategy {
    public GoblnCombatStrategy()
    {
    }
    public void Steer(BasicSteering parent)
    {
        GoblinSteering steering = parent as GoblinSteering;
        if (steering.Targets.Count == 0)
            return;
        while(steering.Targets[0]==null)
        {
            steering.RemoveTarget(steering.Targets[0]);
            if (steering.Targets.Count == 0)
                return;
        }
       parent.SendMessage("Kick", steering.Targets[0]);
    }


    public virtual void ExitState(BasicSteering controller)
    {
        
    }

    public virtual void EnterState(BasicSteering controller)
    {
        
    }
}
