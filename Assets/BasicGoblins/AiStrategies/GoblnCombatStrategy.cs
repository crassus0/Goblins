using UnityEngine;
using System.Collections.Generic;

public class GoblnCombatStrategy : BasicSteeringStrategy {
    BasicSteering m_parent;
    public GoblnCombatStrategy(BasicSteering parent)
    {
        m_parent = parent;
    }
    public void Steer()
    {
        GoblinSteering steering = m_parent as GoblinSteering;
        if (steering.Targets.Count == 0)
            return;
        while(steering.Targets[0]==null)
        {
            steering.RemoveTarget(steering.Targets[0]);
            if (steering.Targets.Count == 0)
                return;
        }
        m_parent.SendMessage("Kick", steering.Targets[0]);
    }
}
