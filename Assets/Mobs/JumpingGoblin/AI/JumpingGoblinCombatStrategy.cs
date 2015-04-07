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

    public static readonly HashSet<string> TargetJumpTags = new HashSet<string>(new string[] { "ShootingObject", "DestroyableObject"});

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
        if (steering.Targets.Count == 0)
        {
            steering.SetStrategy(JumpingGoblinMoveStrategy.Instance());

        }
        else
        {
            GameObject mainTarget = steering.Targets[0];
            if (TargetJumpTags.Contains(mainTarget.tag) && mainTarget.GetComponent<Collider2D>().bounds.min.x - steering.GetComponent<Collider2D>().bounds.max.x > 1.5)
            {
                controller.GetComponent<JumpingGoblinLocomotion>().Jump(new Vector2(mainTarget.GetComponent<Collider2D>().bounds.size.y/4, mainTarget.GetComponent<Collider2D>().bounds.size.y));
            }
            else if (mainTarget.GetComponent<Collider2D>().bounds.min.x - steering.GetComponent<Collider2D>().bounds.max.x > 0.1)
            {
                steering.GetComponent<GoblinLocomotion>().MoveForward(20 * Time.deltaTime / Time.fixedDeltaTime);
            }
            else if (mainTarget.tag != "Enemy")
            {
                controller.GetComponent<GoblinLocomotion>().Kick(mainTarget);
            }
        }
        
        
    }
}
