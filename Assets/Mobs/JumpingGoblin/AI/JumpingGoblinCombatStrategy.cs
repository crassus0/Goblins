using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class JumpingGoblinCombatStrategy : GoblinCombatStrategy
{
    new public static JumpingGoblinCombatStrategy Instance()
    {
        if (s_strategy == null)
            s_strategy = new JumpingGoblinCombatStrategy();
        return s_strategy;
    }
    static JumpingGoblinCombatStrategy s_strategy;

    

    protected JumpingGoblinCombatStrategy()
    {
    }
    public override void SteerOther(BasicSteering controller)
    {
        GoblinSteering steering = controller as GoblinSteering;
        if (steering.SurfaceContact.normal == Vector2.zero)
        {
            steering.SetStrategy(JumpingGoblinFloatStrategy.Instance());
        }
        CheckTargets(steering,2);
        if (steering.Targets.Count == 0 || steering.Targets[0].tag == "Enemy" || steering.Targets[0].GetComponent<Collider2D>().bounds.min.x - steering.GetComponent<Collider2D>().bounds.max.x > 1.5)
        {
            steering.SetStrategy(JumpingGoblinMoveStrategy.Instance());

        }
        else
        {

            
           
                steering.GetComponent<GoblinLocomotion>().Kick(steering.Targets[0].GetComponent<PhysicsObject>());
        }
        
        
        
    }
}
